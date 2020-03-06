namespace ODataModel
{
    public class ChildDto
    {
        public int Key { get; set; }

        public int ParentKey { get; set; }

        public virtual ParentModel ParentModel { get; set; }

    }
}
