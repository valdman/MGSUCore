using System;

namespace Common.Entities
{
    public class UserProfile
    {
        public DateTimeOffset UniversityGraduationYaer { get; private set; }
        public DateTimeOffset Birthyear { get; private set; }
        public string InstitutionName { get; private set; }
    }
}