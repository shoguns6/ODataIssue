using System;
using System.Collections.Generic;

namespace ODataModel
{
    public class ParentDto
    {
        public int Key { get; set; }

        public string Name { get; set; }

        public ICollection<ChildDto> Childrens { get; set; }
    }
}
