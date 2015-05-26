using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Dao.Models;

namespace Yutai.IService
{
    public interface ICategoryItemsRepo
    {
        List<CategoryItems> GetCategoryItems();
        List<CategoryItems> GetCategoryItems(int categoryId,int pageIndex,int pageSize,out int total);
        bool SaveCategoryItems(CategoryItems entity);
        bool Del(int id);
        bool Update(CategoryItems entity);
        CategoryItems GetSingle(int id);
        CategoryItems GetSingleByName(string  categoryItemName);
        
    }
}
