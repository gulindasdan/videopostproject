using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Map;
using VideoPostProject.Model.Entities;

namespace VideoPostProject.Model.Map
{
    public class CategoryMap : CoreMap<Category>
    {
        public CategoryMap()
        {
            ToTable("dbo.Categories");
            Property(x => x.CategoryName).HasColumnName("CategoryName").HasMaxLength(200).IsRequired();
            Property(x => x.CategoryDescription).HasColumnName("CategoryDescription").HasMaxLength(300).IsOptional();
        }
    }
}
