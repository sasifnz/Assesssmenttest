using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//model will convert json response into strongly type model class //
namespace Assesssmenttest.Models
{
    public class Todo
    {
        public int userId { get; set;} // access modifier//
        public int id { get; set; }
        public string title { get; set;}
        public bool completed { get; set; }
        /*{
          "userId": 1,
          "id": 1,
          "title": "delectus aut autem",
          "completed": false } */
    }
}
