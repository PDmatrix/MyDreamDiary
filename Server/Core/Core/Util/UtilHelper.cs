namespace Core.Util
{
    public static class UtilHelper
    {
        public static T GetValueFromAnonymousType<T>(object dataItem, string itemKey ) 
        {
            var type = dataItem.GetType();
            return (T)type.GetProperty(itemKey).GetValue(dataItem, null);
        }
    }
}