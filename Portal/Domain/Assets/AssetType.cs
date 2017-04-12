using Portal.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Domain.Assets
{
    public sealed class AssetType : AggregateRoot
    {
        #region Events
        public event EventHandler<AssetTypeEventArgs> PropertyMoved;
        public event EventHandler<AssetTypeEventArgs> PropertyAdded;
        public event EventHandler<AssetTypeEventArgs> PropertyRemoved;
        #endregion

        #region Properties
        public string Name { get; private set; }
        public IReadOnlyList<string> Properties { get; private set; }
        public IReadOnlyList<Asset> Assets { get; private set; } = new List<Asset>();
        #endregion

        #region Ctors
        public AssetType(string name, List<string> properties, Guid? id = null)
        {
            if (id != null)
                Id = (Guid)id;
            Name = ValidateName(name, 50) ?
                name : throw new ArgumentException("The name is invalid");
            Properties = ValidateProperties(properties) ?
                properties : throw new ArgumentException("The properties are invalid");
        }
        #endregion

        #region Methods

        public void AddAsset(Asset asset)
        {
            if (asset.Values.Count != Properties.Count)
                throw new ArgumentException("Numbers of properties in asset type and values in asset should be equal");

            PropertyMoved += asset.OnPropertyMoved;
            PropertyAdded += asset.OnPropertyAdded;
            PropertyRemoved += asset.OnPropertyRemoved;

            var newAssets = Assets.ToList();
            newAssets.Add(asset);
            Assets = newAssets;
        }

        public void RemoveAsset(Asset asset)
        {
            var newAssets = Assets.ToList();
            newAssets.Remove(asset);
            Assets = newAssets;
        }

        public static bool ValidateName(string name, int maxLength) =>
        !string.IsNullOrEmpty(name) &&
        !string.IsNullOrWhiteSpace(name) &&
        name.Length <= maxLength;

        public static bool ValidateProperties(List<string> properties) =>
            properties.Count > 0 &&
            !properties.GroupBy(n => n).Any(c => c.Count() > 1) &&
            properties.TrueForAll(a => ValidateName(a, 50));

        public void UpdateName(string newName)
        {
            Name = ValidateName(newName, 50) ?
              newName : throw new ArgumentException("The name is invalid");
        }

        public void RenameProperty(string name, string newName)
        {
            if (!Properties.Contains(name))
                throw new ArgumentException("Specified property does not exist");
            if (Properties.Contains(newName))
                throw new ArgumentException("The property with new name already exists");

            var newProperties = Properties.ToList();
            newProperties[Properties.ToList().IndexOf(name)] = newName;
            Properties = newProperties;
        }

        public void MoveProperty(string name, int newIndex)
        {
            if (!Properties.Contains(name))
                throw new ArgumentException("Specified property does not exist");
            if (newIndex < 0 || newIndex >= Properties.Count)
                throw new IndexOutOfRangeException();

            var newProperties = Properties.ToList();

            int propertyIndex = newProperties.IndexOf(name);

            if (newIndex > propertyIndex)
                newIndex--;

            newProperties.Remove(name);
            newProperties.Insert(newIndex, name);
            Properties = newProperties;

            PropertyMoved?.Invoke(this, new AssetTypeEventArgs
            {
                NewIndex = newIndex,
                PropertyIndex = propertyIndex
            });
        }

        public void AddProperty(string name)
        {
            AddProperty(name, Properties.Count);
        }

        public void AddProperty(string name, int index)
        {
            if (Properties.Contains(name))
                throw new ArgumentException("The property with the specified name already exists");
            if (index < 0 || index > Properties.Count)
                throw new IndexOutOfRangeException();

            var newProperties = Properties.ToList();
            newProperties.Insert(index, name);
            Properties = newProperties;

            PropertyAdded?.Invoke(this, new AssetTypeEventArgs
            {
                PropertyIndex = index
            });
        }

        public void RemoveProperty(string name)
        {
            if (!Properties.Contains(name))
                throw new ArgumentException("Specified property does not exist");

            var newProperties = Properties.ToList();
            int propertyIndex = newProperties.IndexOf(name);
            newProperties.RemoveAt(propertyIndex);
            Properties = newProperties;

            PropertyRemoved?.Invoke(this, new AssetTypeEventArgs
            {
                PropertyIndex = propertyIndex
            });
        }
        #endregion
    }
}
