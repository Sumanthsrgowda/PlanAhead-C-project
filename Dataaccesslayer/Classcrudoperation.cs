using WebApplication1.Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks.Dataflow;
using Aspose.Email.PersonalInfo;
using System.Drawing;

namespace WebApplication1.Dataaccesslayer
{
    public class Classcrudoperation
    {
        public loginmodel logging(loginmodel loginmodel)
        {

            DataTable table = new DataTable();
            loginmodel log = new loginmodel();
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "select * from login where email = @email and password = @password";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@email", loginmodel.email);
                cmd.Parameters.AddWithValue("@password", loginmodel.password);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                log.UserName = Convert.ToString(row["username"]);
                log.role = Convert.ToInt32(row["role"]);
            }
            return log;
        }
        public signupmodel signing(signupmodel signupmodel)
        {
            DataTable table = new DataTable();
            int result = 0;
            signupmodel log = new signupmodel();
            log.stdvalid = 0;
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "select * from student where regno = @regno";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@regno", signupmodel.regno);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);

            }
            foreach (DataRow row in table.Rows)
            {

                log.stdname = Convert.ToString(row["stdname"]);
                log.stdbranch = Convert.ToString(row["stdbranch"]);
                log.stdyear = Convert.ToString(row["stdyear"]);
            }
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "select * from login where email = @email";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@email", signupmodel.email);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                log.email = Convert.ToString(row["email"]);
            }
            if (log.stdname != null && log.email == "")
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    string sql2 = "insert into login(username,email,password,role) values(@username,@email,@password,3)";
                    SqlCommand cmd1 = new SqlCommand(sql2, conn);
                    conn.Open();
                    cmd1.Parameters.AddWithValue("@username", signupmodel.regno);
                    cmd1.Parameters.AddWithValue("@email", signupmodel.email);
                    cmd1.Parameters.AddWithValue("@password", signupmodel.password);
                    result = cmd1.ExecuteNonQuery();
                    log.stdvalid = 1;
                }
            }
            if (result != 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    log.regno = signupmodel.regno;
                }
            }
            if (log.email == signupmodel.email)
            {
                log.stdvalid = 2;
            }
            return log;
        }

        public List<eventmodel> getevents()
        {
            DataTable table = new DataTable();
            List<eventmodel> eventlist = new List<eventmodel>();
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "select * from event where completion='Not Complete' and status='yes'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                foreach (DataRow row in table.Rows)
                {
                    eventmodel eventObj = new eventmodel();
                    eventObj.Id = Convert.ToInt32(row["id"]);
                    eventObj.EventName = Convert.ToString(row["ename"]);
                    eventObj.EventDept = Convert.ToString(row["edept"]);
                    eventObj.Description = Convert.ToString(row["edes"]);
                    eventObj.Date = Convert.ToString(row["edate"]);
                    eventObj.Response = Convert.ToString(row["eresponse"]);
                    eventObj.Poster = Convert.ToString(row["poster"]);
                    eventlist.Add(eventObj);

                }
                return eventlist;
            }
        }

        public eventmodel GetEvent(int id)
        {
            DataTable table = new DataTable();
            eventmodel eventObj = new eventmodel();
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "select * from event where id=@id ";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {


                eventObj.Id = Convert.ToInt32(row["id"]);
                eventObj.EventName = Convert.ToString(row["ename"]);
                eventObj.EventDept = Convert.ToString(row["edept"]);
                eventObj.Description = Convert.ToString(row["edes"]);
                eventObj.Date = Convert.ToString(row["edate"]);
                eventObj.Response = Convert.ToString(row["eresponse"]);
                eventObj.Report = Convert.ToString(row["report"]);
                eventObj.Poster = Convert.ToString(row["poster"]);
                eventObj.Status = Convert.ToString(row["completion"]);
            }
            return eventObj;
        }
        public int saveres(eventmodel eventmodel)
        {
            int rows = 0;
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "update event set eresponse=@res where id = @id";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", eventmodel.Id);
                cmd.Parameters.AddWithValue("@res", eventmodel.Response);
                rows = cmd.ExecuteNonQuery();
            }
            return rows;
        }
        public int saveevent(eventmodel eventmodel)
        {
            int rows = 0;
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "insert into event(ename,edes,edept,edate,poster,completion,status) values(@ename,@edes,@edept,@edate,@poster,'Not Complete','pending')";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ename", eventmodel.EventName);
                cmd.Parameters.AddWithValue("@edes", eventmodel.Description);
                cmd.Parameters.AddWithValue("@edate", eventmodel.Date);
                cmd.Parameters.AddWithValue("@edept", eventmodel.EventDept);
                cmd.Parameters.AddWithValue("@poster", eventmodel.Poster);
                rows = cmd.ExecuteNonQuery();
            }
            return rows;
        }
        public List<deptmodel> getdepartment()
        {
            DataTable table = new DataTable();
            List<deptmodel> eventlist = new List<deptmodel>();
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "select * from dept";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                foreach (DataRow row in table.Rows)
                {
                    deptmodel eventObj = new deptmodel();
                    eventObj.DepartmentName = Convert.ToString(row["deptname"]);
                    eventlist.Add(eventObj);

                }
                return eventlist;
            }
        }
        public List<eventmodel> eventsbydept(string name)
        {
            DataTable table = new DataTable();
            List<eventmodel> eventlist = new List<eventmodel>();
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "select * from event where edept=@deptname and completion='Not Complete' and status='yes'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@deptname", name);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {

                eventmodel eventObj = new eventmodel();
                eventObj.Id = Convert.ToInt32(row["id"]);
                eventObj.EventName = Convert.ToString(row["ename"]);
                eventObj.EventDept = Convert.ToString(row["edept"]);
                eventObj.Description = Convert.ToString(row["edes"]);
                eventObj.Date = Convert.ToString(row["edate"]);
                eventObj.Response = Convert.ToString(row["eresponse"]);
                eventObj.Report = Convert.ToString(row["report"]);
                eventObj.Poster = Convert.ToString(row["poster"]);
                eventlist.Add(eventObj);
            }
            return eventlist;
        }
        public int addDept(loginmodel loginmodel)
        {
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string sql2 = "insert into login(username,email,password,role) values(@username,@email,@password,2)";
                SqlCommand cmd1 = new SqlCommand(sql2, conn);
                conn.Open();
                cmd1.Parameters.AddWithValue("@username", loginmodel.UserName);
                cmd1.Parameters.AddWithValue("@email", loginmodel.email);
                cmd1.Parameters.AddWithValue("@password", loginmodel.password);
                cmd1.ExecuteNonQuery();
            }
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string sql1 = "insert into dept(deptname) values(@deptname)";
                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                conn.Open();
                cmd1.Parameters.AddWithValue("@deptname", loginmodel.UserName);
                cmd1.ExecuteNonQuery();
            }
            return 0;
        }
        public void deldept(deptmodel deptmodel)
        {
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string sql1 = "delete from dept where deptname=@deptname";
                string sql2 = "delete from login where username=@deptname";
                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                conn.Open();
                cmd1.Parameters.AddWithValue("@deptname", deptmodel.DepartmentName);
                cmd2.Parameters.AddWithValue("@deptname", deptmodel.DepartmentName);
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
            }
        }
        public int deleteevent(eventmodel eventmodel)
        {
            int result = 0;
            Classcrudoperation op = new Classcrudoperation();
            eventmodel.EventDept = op.gettempevent();
            eventmodel eventObj = new eventmodel();
            DataTable table = new DataTable();
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "select * from event where ename=@name and edept=@dept";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", eventmodel.EventName);
                cmd.Parameters.AddWithValue("@dept", eventmodel.EventDept);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                eventObj.Id = Convert.ToInt32(row["id"]);
            }
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string sql1 = "delete from event where ename=@name AND edept=@dept";
                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                conn.Open();
                cmd1.Parameters.AddWithValue("@name", eventmodel.EventName);
                cmd1.Parameters.AddWithValue("@dept", eventmodel.EventDept);
                result=cmd1.ExecuteNonQuery();
                
            }
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string sql1 = "delete from register where eventid=@id";
                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                conn.Open();
                cmd1.Parameters.AddWithValue("@id", eventObj.Id);
                cmd1.ExecuteNonQuery();

            }
            return result;

        }
        public void addreport(eventmodel eventmodel)
        {
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "update event set report=@report,completion='Completed' where id = @id";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", eventmodel.Id);
                cmd.Parameters.AddWithValue("@report", eventmodel.Report);
                cmd.ExecuteNonQuery();
            }
            
        }
        public int addtempevent(string name)
        {
            int rows = 0;
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=temperdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "update temp set name=@ename";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ename", name);
                rows = cmd.ExecuteNonQuery();
            }
            return rows;
        }
        public int addtempstd(string name)
        {
            int rows = 0;
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=temperdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "update temp set regno=@ename";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ename", name);
                rows = cmd.ExecuteNonQuery();
            }
            return rows;
        }
        public string gettempevent()
        {
            DataTable table = new DataTable();
            string temp=" ";
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=temperdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "select * from temp";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
            }
                foreach (DataRow row in table.Rows)
                {


                    temp = Convert.ToString(row["name"]);

                }
            return temp;
        }
        public int gettempstd()
        {
            DataTable table = new DataTable();
            int temp =0;
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=temperdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "select * from temp";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {


                temp = Convert.ToInt32(row["regno"]);

            }
            return temp;
        }
        public int register(int reg,int id)
        {
            var result = 0;
            DataTable table = new DataTable();
            signupmodel signupmoden=new signupmodel();
            Classcrudoperation op = new Classcrudoperation();
            var obj = op.Getstudent(reg);
            var eve = op.GetEvent(id);
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string sql1 = "select * from register where regno=@reg and name=@name";
                conn.Open();
                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                cmd1.Parameters.AddWithValue("@reg", reg);
                cmd1.Parameters.AddWithValue("@name", eve.EventName);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd1);
                adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                signupmoden.regno = Convert.ToInt32(row["regno"]);
                signupmoden.stdname= Convert.ToString(row["name"]);
            }
            if (reg != signupmoden.regno && eve.EventName != signupmoden.stdname && eve.Status != "Completed")
                {
                    string sql = "insert into register(regno,name,dept,eventid) values(@reg,@name,@dept,@eventid)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@reg", reg);
                    cmd.Parameters.AddWithValue("@name", eve.EventName);
                    cmd.Parameters.AddWithValue("@dept", obj.stdname);
                    cmd.Parameters.AddWithValue("@eventid", id);
                    result = cmd.ExecuteNonQuery();
                }
                if (eve.Status == "Completed")
                {
                    result = 10;
                }
            }
            return result;
        }
        public signupmodel Getstudent(int reg)
        {
            DataTable table = new DataTable();
            signupmodel stdObj = new signupmodel();
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "select * from student where regno=@regno ";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@regno",reg);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {


                stdObj.regno= Convert.ToInt32(row["regno"]);
                stdObj.stdname = Convert.ToString(row["stdname"]);
                stdObj.stdbranch = Convert.ToString(row["stdbranch"]);
                stdObj.stdyear = Convert.ToString(row["stdyear"]);
            }
            return stdObj;
        }
        public List<eventmodel> myevents()
        {
            Classcrudoperation classcrudoperation = new Classcrudoperation();
            var reg = classcrudoperation.gettempstd();
            DataTable table = new DataTable();
            List<eventmodel> eventlist = new List<eventmodel>();
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "select * from register where regno=@regno";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@regno", reg);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                
                foreach (DataRow row in table.Rows)
                {
                    eventmodel eventObj = new eventmodel();
                    eventObj.Id = Convert.ToInt32(row["eventid"]);
                    eventObj.EventName = Convert.ToString(row["name"]);
                    eventlist.Add(eventObj);

                }
                return eventlist;
            }
        }
        public void removestdevent(int reg,int id)
        {
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string sql1 = "delete from register where regno=@reg AND eventid=@id";
                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                conn.Open();
                cmd1.Parameters.AddWithValue("@reg", reg);
                cmd1.Parameters.AddWithValue("@id", id);
                cmd1.ExecuteNonQuery();
            }
        }
        public List<signupmodel> getstudentsbyevents(int id)
        {
            DataTable table = new DataTable();
            List<signupmodel> stdlist = new List<signupmodel>();
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "select * from register where eventid=@id";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {

                signupmodel eventObj = new signupmodel();
                eventObj.regno = Convert.ToInt32(row["regno"]);
                eventObj.stdname = Convert.ToString(row["dept"]);
                stdlist.Add(eventObj);
            }
            return stdlist;
        }
        public List<eventmodel> completedeventsbydept(string name)
        {
            DataTable table = new DataTable();
            List<eventmodel> eventlist = new List<eventmodel>();
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "select * from event where edept=@deptname and completion='Completed' and status='yes'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@deptname", name);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {

                eventmodel eventObj = new eventmodel();
                eventObj.Id = Convert.ToInt32(row["id"]);
                eventObj.EventName = Convert.ToString(row["ename"]);
                eventObj.EventDept = Convert.ToString(row["edept"]);
                eventObj.Description = Convert.ToString(row["edes"]);
                eventObj.Date = Convert.ToString(row["edate"]);
                eventObj.Response = Convert.ToString(row["eresponse"]);
                eventObj.Report = Convert.ToString(row["report"]);
                eventObj.Poster = Convert.ToString(row["poster"]);
                eventlist.Add(eventObj);
            }
            return eventlist;
        }
        public List<eventmodel> getcompletedevent()
        {
            DataTable table = new DataTable();
            List<eventmodel> eventlist = new List<eventmodel>();
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "select * from event where completion='Completed' and status='yes'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                foreach (DataRow row in table.Rows)
                {
                    eventmodel eventObj = new eventmodel();
                    eventObj.Id = Convert.ToInt32(row["id"]);
                    eventObj.EventName = Convert.ToString(row["ename"]);
                    eventObj.EventDept = Convert.ToString(row["edept"]);
                    eventObj.Description = Convert.ToString(row["edes"]);
                    eventObj.Date = Convert.ToString(row["edate"]);
                    eventObj.Response = Convert.ToString(row["eresponse"]);
                    eventObj.Poster = Convert.ToString(row["poster"]);
                    eventlist.Add(eventObj);

                }
                return eventlist;
            }
        }
        public List<eventmodel> pendingeventsbydept(string name)
        {
            DataTable table = new DataTable();
            List<eventmodel> eventlist = new List<eventmodel>();
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "select * from event where edept=@deptname and status='pending'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@deptname", name);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {

                eventmodel eventObj = new eventmodel();
                eventObj.Id = Convert.ToInt32(row["id"]);
                eventObj.EventName = Convert.ToString(row["ename"]);
                eventObj.EventDept = Convert.ToString(row["edept"]);
                eventObj.Description = Convert.ToString(row["edes"]);
                eventObj.Date = Convert.ToString(row["edate"]);
                eventObj.Response = Convert.ToString(row["eresponse"]);
                eventObj.Report = Convert.ToString(row["report"]);
                eventObj.Poster = Convert.ToString(row["poster"]);
                eventlist.Add(eventObj);
            }
            return eventlist;
        }
        public List<eventmodel> deniedeventsbydept(string name)
        {
            DataTable table = new DataTable();
            List<eventmodel> eventlist = new List<eventmodel>();
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "select * from event where edept=@deptname and status='no'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@deptname", name);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {

                eventmodel eventObj = new eventmodel();
                eventObj.Id = Convert.ToInt32(row["id"]);
                eventObj.EventName = Convert.ToString(row["ename"]);
                eventObj.EventDept = Convert.ToString(row["edept"]);
                eventObj.Description = Convert.ToString(row["edes"]);
                eventObj.Date = Convert.ToString(row["edate"]);
                eventObj.Response = Convert.ToString(row["eresponse"]);
                eventObj.Report = Convert.ToString(row["report"]);
                eventObj.Poster = Convert.ToString(row["poster"]);
                eventlist.Add(eventObj);
            }
            return eventlist;
        }
        public List<eventmodel> getpendingevent()
        {
            DataTable table = new DataTable();
            List<eventmodel> eventlist = new List<eventmodel>();
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "select * from event where status='pending'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                foreach (DataRow row in table.Rows)
                {
                    eventmodel eventObj = new eventmodel();
                    eventObj.Id = Convert.ToInt32(row["id"]);
                    eventObj.EventName = Convert.ToString(row["ename"]);
                    eventObj.EventDept = Convert.ToString(row["edept"]);
                    eventObj.Description = Convert.ToString(row["edes"]);
                    eventObj.Date = Convert.ToString(row["edate"]);
                    eventObj.Response = Convert.ToString(row["eresponse"]);
                    eventObj.Poster = Convert.ToString(row["poster"]);
                    eventlist.Add(eventObj);

                }
                return eventlist;
            }
        }
        public List<eventmodel> getdeniedevent()
        {
            DataTable table = new DataTable();
            List<eventmodel> eventlist = new List<eventmodel>();
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "select * from event where status='no'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                foreach (DataRow row in table.Rows)
                {
                    eventmodel eventObj = new eventmodel();
                    eventObj.Id = Convert.ToInt32(row["id"]);
                    eventObj.EventName = Convert.ToString(row["ename"]);
                    eventObj.EventDept = Convert.ToString(row["edept"]);
                    eventObj.Description = Convert.ToString(row["edes"]);
                    eventObj.Date = Convert.ToString(row["edate"]);
                    eventObj.Response = Convert.ToString(row["eresponse"]);
                    eventObj.Poster = Convert.ToString(row["poster"]);
                    eventlist.Add(eventObj);

                }
                return eventlist;
            }
        }

        public void approve(int id)
        {
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "update event set status='yes' where id = @id";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

        }
        public void deny(int id)
        {
            string connstring = "Data Source=LAPTOP-OQSA31RA\\SQLEXPRESS02;Initial Catalog=managementdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {

                string sql = "update event set status='no' where id = @id";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

        }
    }

}

        



    
