using Portal.Domain.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Domain.Assets
{
    public class Asset : Entity
    {
        #region Properties
        public IReadOnlyList<string> Values { get; private set; } 
        #endregion

        #region Ctors
        public Asset(IReadOnlyList<string> values)
        {
            Values = values;
        } 
        #endregion

        #region EventHandlers
        public void OnPropertyMoved(object sender, AssetTypeEventArgs e)
        {
            List<string> newValues = Values.ToList();
            newValues.Insert(e.NewIndex, newValues[e.PropertyIndex]);
            newValues.RemoveAt(e.PropertyIndex + 1);
            Values = newValues;
        }

        public void OnPropertyAdded(object sender, AssetTypeEventArgs e)
        {
            var newValues = Values.ToList();
            newValues.Insert(e.PropertyIndex, "");
            Values = newValues;
        }

        public void OnPropertyRemoved(object sender, AssetTypeEventArgs e)
        {
            var newValues = Values.ToList();
            newValues.RemoveAt(e.PropertyIndex);
            Values = newValues;
        } 
        #endregion
    }
}
