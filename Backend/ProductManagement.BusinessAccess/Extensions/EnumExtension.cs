namespace ProductManagement.BusinessLayer.Extensions
{
    public static class EnumExtension
    {
        public static int GetValue(this Enum value)
        {
            if (value == null)
                return 0;
            return Convert.ToInt32(value);
        }

    }
}
