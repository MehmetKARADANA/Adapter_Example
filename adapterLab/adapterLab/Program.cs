using System.Collections;
using System.Runtime.ConstrainedExecution;

internal class Program
{
    private static void Main(string[] args)
    {
      
    }
}

interface DB
{
    public User SelectUserById(int id);

    public void insertUser(User user);

}

class User
{
    private string name;
    private int id;

    public User(string name, int id)
    {
        this.SetName(name);
        this.SetId(id);
    }
    /*
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    */
    public string GetName()
    {
        return name;
    }

    public void SetName(string newName)
    {
        name = newName;
    }

    public int GetId()
    {
        return id;
    }

    public void SetId(int newId)
    {
        id = newId;
    }


}

class Oracle : DB
{
    private  List<User> userList;

    private int idCounter;


    public Oracle()
    {
        idCounter = 0;
        userList = new List<User>();
    }

    public List<User> GetUserList()
    {
        return userList;
    }

    public void SetUserList(List<User> newUserList)
    {
        userList = newUserList;
    }

    public int GetIdCounter()
    {
        return idCounter;
    }

    public void SetIdCounter(int newIdCounter)
    {
        idCounter = newIdCounter;
    }

    public void insertUser(User user)
    {
        count();
        user.SetId(idCounter);
        userList.Add(user); 
        
    }

    public User SelectUserById(int id)
    {
        foreach (User user1 in userList)
        {
            if (user1.GetId()==id) {
                return user1;
                    }
            
        }
        return null;

    }

    private void count()
    {
        idCounter=idCounter+1;
    }
}

class Service
{
    DB db;
    public Service(DB db1) {
        this.SetDb(db1);
    }

    public User getUser(int id)
    {
        return db.SelectUserById(id);
    }

    public void addUser(User user)
    {
        db.insertUser(user);
    }

    public DB GetDb()
    {
        return db;
    }

    // Set metodu
    public void SetDb(DB value)
    {
        this.db = value;
    }
}

class Adapter :DB
{
    private Mssql mssql;

    public Adapter()
    {
        this.mssql =new Mssql();
    }

    public void insertUser(User user)
    {
        mssql.User(user);
    }

    public User SelectUserById(int id)
    {
      return  mssql.UserById(id);
        
    }
}

class Mssql
{
    private List<User> userList;

    private int idCounter;

    public Mssql()
    {
        this.userList =new List<User>();
        this.idCounter = 0;
    }

    public User UserById(int id) {
    
        foreach (User user in userList)
        {
            if (user.GetId()==id)
            {
                return user;
            }
        }
        return null;
    
    }

    public void User(User user)
    {
        user.SetId(idCounter++);
        userList.Add(user);
    }
}