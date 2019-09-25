using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Map;
using VideoPostProject.Model.Entities;

namespace VideoPostProject.Model.Map
{
    public class SubCategoryMap : CoreMap<SubCategory>
    {
        public SubCategoryMap()
        {
            ToTable("dbo.SubCategories");
            Property(x => x.SubCategoryName).HasColumnName("SubCategoryName").HasMaxLength(150).IsOptional();
            Property(x => x.Description).HasColumnName("Description").HasMaxLength(250).IsOptional();
        }
    }
}
