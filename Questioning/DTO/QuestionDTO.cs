using System;

namespace Questioning.DTO
{
    public class QuestionDTO
    {
        public TypeAnswer TypeAnswer { get; set; }
        public string Name { get; set; }
        public string StringValue { get; set; }
        public int? IntValue { get; set; }
        public DateTime? DateTimeValue { get; set; }        
    }
   
}
