using System;
using System.Linq;
using Yutai.Dao;

namespace Yutai.Service
{
    public class BaseRepo
    {
        public bool Exec(Action<YutaiDB> func ,bool isSave=false)
        {
            try
            {
                using (YutaiDB db = new YutaiDB())
                {
                    func.Invoke(db);
                    if (isSave)
                    {
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
