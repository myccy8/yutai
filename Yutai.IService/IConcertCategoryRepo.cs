using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Dao.Models;

namespace Yutai.IService
{
    public interface IConcertCategoryRepo
    {
        List<ConcertCategory> GetCategory();
        bool SaveCategory(ConcertCategory entity);
        bool Del(int id);
        bool Update(int id, string name);
        ConcertCategory GetSingle(int id);
    }
}
