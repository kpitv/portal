using Portal.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using Portal.Domain.Assets.Exceptions;
using static Portal.Domain.Shared.ValidationError;

namespace Portal.Domain.Assets
{
    public sealed class AssetType : AggregateRoot
    {
        #region Events
        public event EventHandler<AssetTypeEventArgs> PropertyMoved;
        public event EventHandler<AssetTypeEventArgs> PropertyAdded;
        public event EventHandler<AssetTypeEventArgs> PropertyRemoved;
        public static event EventHandler<ValidationEventArgs> ErrorOccurred;
        #endregion

        #region Properties
        public string Name { get; private set; }
        public IReadOnlyList<string> Properties { get; private set; }
        public IReadOnlyList<Asset> Assets { get; private set; } = new List<Asset>();
        #endregion

        #region Ctors
        public AssetType(string name, List<string> properties)
        {
            bool state = true;
            if (name is null || properties is null)
                throw new ArgumentNullException();
            if (properties.Any(p => p is null))
                throw new ArgumentNullException();
            if (!ValidateName(name, 50))
                state = false;
            if (!ValidateProperties(properties))
                state = false;

            if (!state)
                throw new ArgumentException();

            Name = name;
            Properties = properties;
        }
        #endregion

        #region Methods
        public static AssetType CreateWithId(Guid id, string name, List<string> properties) =>
            new AssetType(name, properties) { Id = id };

        public void AddAsset(Asset asset)
        {
            if (asset.Values.Count != Properties.Count)
                throw new InvalidValuesCountException(nameof(InvalidValuesCountException));

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

        public bool ValidateName(string name, int maxLength)
        {
            bool state = true;
            if (string.IsNullOrWhiteSpace(name) || name.Length > maxLength)
            {
                ErrorOccurred?.Invoke(this, new ValidationEventArgs(InvalidAssetTypeName, nameof(Name)));
                state = false;
            }
            return state;
        }

        public bool ValidateProperties(List<string> properties)
        {
            bool state = true;
            if (properties != null &&
                !properties.Any(p => p is null) &&
                properties.Count > 0 &&
                properties.Distinct().Count() == properties.Count)
            {
                foreach (var property in properties)
                    if (string.IsNullOrWhiteSpace(property) || property.Length > 50)
                    {
                        ErrorOccurred?.Invoke(this, new ValidationEventArgs(InvalidAssetTypeProperty, invalidValue: property));
                        return false;
                    }
            }
            else
            {
                ErrorOccurred?.Invoke(this, new ValidationEventArgs(InvalidAssetTypePropertiesList, nameof(Properties)));
                state = false;
            }
            return state;
        }

        public void UpdateName(string newName)
        {
            Name = ValidateName(newName, 50) ?
              newName : throw new InvalidAssetTypeNameException(nameof(InvalidAssetTypeNameException));
        }

        public void RenameProperty(string name, string newName)
        {
            if (!Properties.Contains(name))
                throw new PropertyNotFoundException(nameof(PropertyNotFoundException));
            if (Properties.Select(p => p.ToUpper()).Contains(newName.ToUpper()))
                throw new InvalidPropertyException(nameof(InvalidPropertyException), "The property with new name already exists");

            var newProperties = Properties.ToList();
            newProperties[Properties.ToList().IndexOf(name)] = newName;
            Properties = newProperties;
        }

        public void MoveProperty(string name, int newIndex)
        {
            if (!Properties.Contains(name))
                throw new PropertyNotFoundException(nameof(PropertyNotFoundException));
            if (newIndex < 0 || newIndex >= Properties.Count)
                throw new IndexOutOfRangeException(nameof(IndexOutOfRangeException));

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
            if (Properties.Select(p => p.ToUpper()).Contains(name.ToUpper()))
                throw new InvalidPropertyException(nameof(InvalidPropertyException), "The property with specified name already exists");
            if (index < 0 || index > Properties.Count)
                throw new IndexOutOfRangeException(nameof(IndexOutOfRangeException));

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
                throw new PropertyNotFoundException(nameof(PropertyNotFoundException));

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
