namespace Api
{
    public static class Utility
    {
        public static int GetAge(DateTime dateOfBirth, DateTime payCheckDate)
        {
            int age = payCheckDate.Year - dateOfBirth.Year;

            if (payCheckDate < dateOfBirth.AddYears(age))
                age--;

            return age;
        }
    }
}
