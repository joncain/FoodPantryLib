using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using log4net;

namespace FoodPantryLib
{
    public class Recipient
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ClassDb db = new ClassDb();
        public int id { get; set; }
        public int parentId { get; set; }
        public string first = string.Empty;
        public string last = string.Empty;
        public DateTime dob;
        public Address address = new Address();
        public Phone phone = new Phone();
        public bool idVerified = false;
        public bool attendsCalvary = false;
        public string errorMessage = string.Empty;
        public bool deleted = false;

        public Recipient()
        {
        }

        public Recipient(int id)
        {
            this.id = id;
            Get();
        }

        public SaveResult Save()
        {
            SaveResult result = new SaveResult();

            try
            {
                if (string.IsNullOrEmpty(first))
                {
                    throw new Exception("Invalid first name");
                }

                if (string.IsNullOrEmpty(last))
                {
                    throw new Exception("Invalid last name");
                }

                if (!string.IsNullOrEmpty(phone.value) && !phone.isValid)
                {
                    throw new Exception("Invalid phone");
                }

                if (dob.Year > DateTime.Now.Year || dob.Year < 1900)
                {
                    throw new Exception("Invalid birth year");
                }

                if (address != null)
                {
                    ValidationResult addressIsValid = address.isValid;

                    if (!addressIsValid.valid)
                    {
                        throw new Exception(addressIsValid.message);
                    }
                }

                //Save Record
                try
                {
                    if (id > 0)
                    {
                        db.Exec(string.Format("UPDATE Recipients SET ParentRecipientID = {0}, First = '{1}', Last = '{2}', Phone = '{3}', Address1 = '{4}', Address2 = '{5}', City = '{6}', State = '{7}', Zipcode = '{8}', IDVerified = {9}, AttendsCalvary = {10}, DOB = '{12}', Deleted = {13} WHERE RowId = {11}", parentId == 0 ? "NULL" : parentId.ToString(), Utility.makeSQLSafe(format(first)), Utility.makeSQLSafe(format(last)), phone.value, Utility.makeSQLSafe(address.address1), Utility.makeSQLSafe(address.address2), Utility.makeSQLSafe(address.city), address.state, address.zipcode, idVerified ? 1 : 0, attendsCalvary ? 1 : 0, id, dob.ToString("yyyy-MM-dd"), (deleted ? "1" : "NULL")));
                    }
                    else
                    {
                        db.Exec(string.Format("INSERT INTO Recipients(ParentRecipientID, First, Last, Phone, Address1, Address2, City, State, Zipcode, IDVerified, AttendsCalvary, DOB) VALUES({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', {9}, {10}, '{11}')", parentId == 0 ? "NULL" : parentId.ToString(), Utility.makeSQLSafe(format(first)), Utility.makeSQLSafe(format(last)), phone.value, Utility.makeSQLSafe(address.address1), Utility.makeSQLSafe(address.address2), Utility.makeSQLSafe(address.city), address.state, address.zipcode, (idVerified ? 1 : 0), (attendsCalvary ? 1 : 0), dob.ToString("yyyy-MM-dd")));
                        db.Exec("select max(RowId) id from recipients");
                        id = Convert.ToInt32(db["id"]);
                    }
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.message = e.Message;
                }
            }
            catch (Exception e)
            {
                result.success = false;
                result.message = e.Message;
            }

            return result;
        }

        public SaveResult RecordVisit()
        {
            SaveResult result = new SaveResult();

            try
            {
                db.Exec(string.Format("SELECT oid FROM Visits WHERE RecipientID = {0} AND strftime('%Y%m%d', VisitDate) = strftime('%Y%m%d', Date());", id));

                if (db.Results.Tables[0].Rows.Count == 0)
                {
                    db.Exec(string.Format("INSERT INTO Visits(RecipientID) SELECT oid FROM Recipients WHERE oid = {0} OR ParentRecipientID = {0}", id));
                }
            }
            catch (Exception e)
            {
                result.success = false;
                result.message = e.Message;
            }

            return result;
        }
        
        public bool Get()
        {
            if (id > 0)
            {
                try
                {
                    if (db.Exec(string.Format("SELECT RowId RecipientID, First, Last, Address1, Address2, City, State, Zipcode, Phone, AttendsCalvary, IDVerified, strftime('%m/%d/%Y', DOB) DOB FROM Recipients WHERE RowId = {0}", id)))
                    {
                        id = Convert.ToInt32(db["RecipientID"]);
                        
                        try
                        {
                            parentId = Convert.ToInt32(db["ParentRecipientID"]);
                        }
                        catch
                        {
                        }

                        first = db["First"];
                        last = db["Last"];
                        phone = new Phone(db["Phone"]);
                        address = new Address { address1 = db["Address1"], address2 = db["Address2"], city = db["City"], state = db["State"], zipcode = db["Zipcode"] };
                        idVerified = db["IDVerified"] == "1" ? true : false;
                        attendsCalvary = db["AttendsCalvary"] == "1" ? true : false;
                        dob = Convert.ToDateTime(db["DOB"].ToString());
                        deleted = (db["Deleted"] == "1" ? true : false);
                    }
                }
                catch (Exception e)
                {
                    errorMessage = e.Message;
                    return false;
                }
            }

            return true;
        }

        public DataSet Dependents
        {
            get
            {
                DataSet dependents = null;

                if (id > 0)
                {
                    try
                    {
                        if (!db.Exec(string.Format("SELECT oid RecipientID, First, Last, strftime('%m/%d/%Y', DOB) [Birth Date] FROM Recipients WHERE ParentRecipientID = {0} AND Deleted IS NULL ORDER BY Last, First ASC", id)))
                        {
                            throw new Exception(db.ErrorMessage);
                        }

                        dependents = db.Results;
                    }
                    catch (Exception e)
                    {
                        log.Error(e);
                    }
                }

                return dependents;
            }
        }

        public DataSet Visits
        {
            get
            {
                DataSet visits = null;

                if (id > 0)
                {
                    try
                    {
                        if (!db.Exec(string.Format("SELECT strftime('%m/%d/%Y', VisitDate) Date FROM Visits WHERE RecipientId = {0} ORDER BY VisitDate DESC", id)))
                        {
                            throw new Exception(db.ErrorMessage);
                        }

                        visits = db.Results;
                    }
                    catch (Exception e)
                    {
                        log.Error(e);
                    }
                }

                return visits;
            }
        }

        private string format(string name)
        {
            name = name.ToLower().Trim(); ;

            char[] chars = name.ToCharArray();

            if (chars.Length > 0)
            {
                chars[0] = char.ToUpper(chars[0]);
            }

            return new string(chars);
        }
    }
}
