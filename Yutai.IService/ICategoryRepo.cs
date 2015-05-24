using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Dao.Models;

namespace Yutai.IService
{
   public interface ICategoryRepo
    {
       List<Category> GetCategory();
       bool SaveCategory(Category entity);
        bool Del(int id);
        bool Update(int id,string  name);
        Category GetSingle(int id);
        Category GetSingleByName(string  name);
    }
}
