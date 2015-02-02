using System;
using System.Collections;
using System.Data.SQLite;
using System.Data;
using System.Configuration;
using log4net;

namespace FoodPantryLib
{
	/// <summary>
	/// Summary description for ClassDb.
	/// </summary>
	public class ClassDb
	{
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private SQLiteConnection mConnection;
		private string mConnectionString = "";
        private string mDatabaseName = ConfigurationSettings.AppSettings["dbPath"];
		private SQLiteCommand mCommand;
		private DataSet mResults = new DataSet();
		private ArrayList mParameters = new ArrayList();
		private int mCommandTimeout = 60;
		private int mRowCount = 0;
        private string mErrorMessage = string.Empty;

		public ClassDb()
		{
            
			///////////////////////////////////////////////////////////////////////
			//
			// Set up the connection
			//
			mConnection = new SQLiteConnection();
			mConnectionString = @"Version=3; UTF8Encoding=True; Data Source=" + mDatabaseName;
            mConnection.ConnectionString = mConnectionString;
		}

		public bool Exec(string Command)
		{
			///////////////////////////////////////////////////////////////////////
			//
			// Set up the command object
			//
			mCommand = mConnection.CreateCommand();
			mCommand.CommandText = Command;
			mCommand.CommandTimeout = mCommandTimeout;
			mCommand.CommandType = CommandType.Text;
			return ExecCommand();
		}

		private bool ExecCommand()
		{
			//////////////////////////////////////////////////////////////////
			//
			// Create the db connection
			//
			mConnection.Open();
			if (mConnection.State != ConnectionState.Open)
			{
				mErrorMessage = "A connection to the database could not be created";
				return false;
			}

			try
			{
				///////////////////////////////////////////////////////////////////////
				//
				// Try to execute the query
				//
				log.Debug(mCommand.CommandText);
				SQLiteDataAdapter mDa = new SQLiteDataAdapter(mCommand.CommandText, mConnection.ConnectionString);
                mResults = new DataSet();
				mDa.Fill(mResults);
				mConnection.Close();

				if (mResults.Tables.Count > 0)
				{
					mRowCount = mResults.Tables[0].Rows.Count;
				}
				else
				{
					mRowCount = 0;
				}
			}
			catch (SQLiteException e)
			{
				///////////////////////////////////////////////////////////////////////
				//
				// Something went wrong. Make the error message available
				//
                log.Error(e);
				mErrorMessage = e.Message;
				return false;
			}
			return true;
		}

        public int CommandTimeout
		{
			get
			{
				return mCommandTimeout;
			}
			set
			{
				mCommandTimeout = value;
			}
		}
	
        public DataSet Results
		{
			get
			{
				return mResults;
			}
		}
	
        public DateTime GetDateTimeValue(string Value)
		{
			DateTime d;
			try
			{
				d = DateTime.Parse(Value);
			}
			catch
			{
				d = DateTime.Now;
			}
			return d;
		}
	
        public int GetIntValue(string Value)
		{
			int i = 0;
			try
			{
				i = Int32.Parse(Value);
			}
			catch
			{
			}
			return i;
		}
	
        public string this[string Column]
		{
			get
			{
				string Value = null;
				try
				{
					Value = mResults.Tables[0].Rows[0][Column].ToString();
				}
				catch (Exception e)
				{
					Value = "Error: " + e.Message;
				}
				return Value;
			}
		}
	
        public int RowCount
		{
			get
			{
				return mRowCount;
			}
		}
	
		public string DatabaseName
		{
			get
			{
				return mDatabaseName;
			}
		}
	
        public SQLiteConnection Connection
		{
			get
			{
				return mConnection;
			}
		}
	
        public bool Connect()
		{
			mConnection.ConnectionString = mConnectionString;
			mConnection.Open();
			if (mConnection.State != ConnectionState.Open)
			{
				mErrorMessage = "A connection to the database could not be established";
				return false;
			}
			return true;
		}
	
        public string ConnectionString
		{
			get
			{
				return mConnectionString;
			}
		}

        public string ErrorMessage
        {
            get
            {
                return mErrorMessage;
            }
        }
	}
}
