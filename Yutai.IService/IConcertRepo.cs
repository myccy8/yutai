using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Dao.Models;

namespace Yutai.IService
{
  public  interface IConcertRepo
    {
      List<Concert> GetConcerts();
      List<Concert> GetConcerts(int categoryId, int pageIndex, int pageSize, out int total);
        bool SaveConcert(Concert entity);
        bool Del(int id);
        bool Update(Concert entity);
        Concert GetSingle(int id);
        bool SetStatus(int type,int id);
    }
}
