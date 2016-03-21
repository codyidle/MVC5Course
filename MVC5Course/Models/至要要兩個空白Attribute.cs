using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVC5Course.Models
{
    public class 至要要兩個空白Attribute : DataTypeAttribute
    {
        public 至要要兩個空白Attribute() : base(DataType.Text)
        {

        }

        public override bool IsValid(object value)
        {
            string str = (string)value;
            return str.Count(p => p == ' ') >= 2;
        }


    }
}