namespace LinkDev.IKEA.DataAccessLayer.Models
{
    public class Department : ModelBase
    {
        /*[Required(ErrorMessage = "name is required")]*/ 
        // اول اشي لازم ما اكتب ريكوايرد لانه السترنج باي ديفولت هو ريكوايرد 
        // ثاني اشي باي ديفولت بكوم في ايرور مسج ولو مش عاجبتني فما بغيرها هون لانه هذا المودل وبغيرها بالفيو مودل 
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }

        public DateTime CreationDate { get; set; } // الوقت الي انعمل فيه قسم معين بالشركه 

        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();


    }
}
