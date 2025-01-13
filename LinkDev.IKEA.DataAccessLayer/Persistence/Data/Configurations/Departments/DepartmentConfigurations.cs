using LinkDev.IKEA.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DataAccessLayer.Persistence.Data.Configrations.Departments
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Department> builder)
        {
            // بنكونفيجر موديل الديبارتمنت

            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(D => D.Code).HasColumnType("varchar(20)").IsRequired(); // في نوع اسمه سيكوانس بجينيرات كود بطريقه منظمه انا بختارها

            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
            // HasDefaultValueSql   ==> بتحدد الديفولت فاليو للريكورد اول ما نعمل الريكورد بتوخذ القيمه 
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GETDATE()");
            // HasComputedColumnSql ==> بتنفذ الكود تبعها بكل مره بعمل فيها تحديث للريكورد 
            builder.HasMany(D => D.Employees)
                   .WithOne(E => E.Department)
                   .HasForeignKey(E => E.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull);
        }

    }
}
