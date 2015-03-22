using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace FoodPantryLib
{
    public class Search
    {
        public DataSet DoSearch(string searchText)
        {
            ClassDb db = new ClassDb();
            db.Exec(string.Format("SELECT R.rowid RecipientID, R.First || ' ' || R.Last Name,  COUNT(D.rowid) +1 HouseholdCount, strftime('%m/%d/%Y', R.DOB) BirthDate, R.Phone, R.Address1, R.City FROM Recipients R LEFT JOIN Recipients D ON (R.rowid = D.ParentRecipientID AND D.Deleted IS NULL) WHERE R.Deleted IS NULL AND (R.First LIKE '%{0}%' OR R.Last LIKE '%{0}%' OR R.Phone LIKE '%{0}%' )AND R.ParentRecipientID IS NULL GROUP BY R.rowid, Name, BirthDate, R.Phone, R.Address1, R.City", searchText.Replace(" ", string.Empty)));
            return db.Results;
        }

    }
}
