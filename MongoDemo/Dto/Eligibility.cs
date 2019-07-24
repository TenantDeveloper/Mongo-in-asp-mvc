namespace MongoDemo.Dto
{
    public enum Gender
    {
        male = 1, female , both
    }
    
    public enum Grade
    {
        manager = 1, pm , senior , junior
    }
    public class Eligibility
    {
        public Gender Gender { get; set; } = (Gender)3;
        public bool IsSaudi { get; set; }
        public Grade Grade { get; set; }
    }
}