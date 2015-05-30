using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Dao.Models;

namespace Yutai.IService
{
    public interface IIndexRepo
    {
        List<HomeEntity> GetHomeImage(string typeId);
        List<HomeEntity> GetHomeImage();
        bool SaveHomeImage(HomeEntity entity);
        bool DelHomeImage(int id);
        bool UpdateHomeImage(HomeEntity entity);
        HomeEntity GetSingle(int id);
    }
}
