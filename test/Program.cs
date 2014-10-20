using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMapper;
namespace test
{

    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        
        public string SchoolName { get; set; }
        public int Grade { get; set; }

    }


    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person
            {
                ID = 1,
                Name = "Mehdi",
                Address = "Sydney"
            };

            Student student = new Student
            {
                ID = 2,
                Name = "Mo",
                Address="Seven Hills",
                Grade = 1,
                SchoolName = "UTS"
            };

            //Console.Write(student.Name);

            Person newStudent = SimpleMapper.SimpleMapper.Map2<Student, Person>(student);
            Console.Write(newStudent.Address);

            Console.ReadLine();

        }
    }
}
