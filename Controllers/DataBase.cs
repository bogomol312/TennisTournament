using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TurniejTenisowy.Models;

namespace TurniejTenisowy.Controllers
{
    public class DataBase
    {                           //connection String
        private string constring = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True";

        public DataBase(){
        }

        // <<------------------------------------ TABELA ZAWODNIK ----------------------------->>
        public bool insertZawodnik(Zawodnik z)
        {
          if(z.Trener != 0)
            using (SqlConnection connection = new SqlConnection(constring)){
                connection.Open();
                //exec TINPROaddNewZawodnik 'Legia','Warszawa','2019-11-17','PL','K','6';

                using (SqlCommand command = new SqlCommand("exec TINPROaddNewZawodnik @imie,@nazwisko,@dataurod,@kraj,@plec,@trener", connection)){
                    command.Parameters.AddWithValue("imie", z.Imie);
                    command.Parameters.AddWithValue("nazwisko", z.Nazwisko);
                    command.Parameters.AddWithValue("kraj", z.Kraj);
                    command.Parameters.AddWithValue("dataurod", z.DataUrodzenia);
                    command.Parameters.AddWithValue("plec", z.Plec);
                    command.Parameters.AddWithValue("trener",z.Trener);

                    try{
                        int affectedRows = command.ExecuteNonQuery();//Console.Write(affectedRows);   
                    }
                     catch (SqlException) {
                        return false;
                    }

                    return true;
                }  
            }
            else
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();
                    //exec TINPROaddNewZawodnik 'Legia','Warszawa','2019-11-17','PL','K','6';

                    using (SqlCommand command = new SqlCommand("exec TINPROaddNewZawodnik @imie,@nazwisko,@dataurod,@kraj,@plec,@trener", connection))
                    {
                        command.Parameters.AddWithValue("imie", z.Imie);
                        command.Parameters.AddWithValue("nazwisko", z.Nazwisko);
                        command.Parameters.AddWithValue("kraj", z.Kraj);
                        command.Parameters.AddWithValue("dataurod", z.DataUrodzenia);
                        command.Parameters.AddWithValue("plec", z.Plec);
                        command.Parameters.AddWithValue("trener", DBNull.Value);

                        try
                        {
                            int affectedRows = command.ExecuteNonQuery();//Console.Write(affectedRows);   
                        }
                        catch (SqlException)
                        {
                            return false;
                        }

                        return true;
                    }
                }
        }

        public bool updateZawodnik(Zawodnik z)
        {

            if(z.Trener != 0)
                try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    //exec TINPROupdZawodnik '34','kokrus','pokrus','2019-11-19','PL','K',null;
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("exec TINPROupdZawodnik @idgosc,@imie,@nazwisko,@dataurod,@kraj,@plec,@trener", connection))
                    {
                        command.Parameters.AddWithValue("idgosc", z.IdGosc);
                        command.Parameters.AddWithValue("imie", z.Imie);
                        command.Parameters.AddWithValue("nazwisko", z.Nazwisko);
                        command.Parameters.AddWithValue("dataurod", z.DataUrodzenia);
                        command.Parameters.AddWithValue("kraj", z.Kraj);
                        command.Parameters.AddWithValue("plec", z.Plec);
                        command.Parameters.AddWithValue("trener", z.Trener); // == 0 ? DBNull.Value : z.Trener);

                        int affectedRows = command.ExecuteNonQuery();
                        //  Console.WriteLine(affectedRows);
                    }
                    return true;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            else
                try
                {
                    using (SqlConnection connection = new SqlConnection(constring))
                    {
                        //exec TINPROupdZawodnik '34','kokrus','pokrus','2019-11-19','PL','K',null;
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("exec TINPROupdZawodnik @idgosc,@imie,@nazwisko,@dataurod,@kraj,@plec,@trener", connection))
                        {
                            command.Parameters.AddWithValue("idgosc", z.IdGosc);
                            command.Parameters.AddWithValue("imie", z.Imie);
                            command.Parameters.AddWithValue("nazwisko", z.Nazwisko);
                            command.Parameters.AddWithValue("dataurod", z.DataUrodzenia);
                            command.Parameters.AddWithValue("kraj", z.Kraj);
                            command.Parameters.AddWithValue("plec", z.Plec);
                            command.Parameters.AddWithValue("trener", DBNull.Value); 

                            int affectedRows = command.ExecuteNonQuery();
                        }
                        return true;
                    }
                }
                catch (SqlException)
                {
                    return false;
                }

        }

        public Zawodnik getZawodnikById(int? id)
        {
            Zawodnik zawodnik = new Zawodnik();

            using (SqlConnection connection = new SqlConnection(constring))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("select IdGosc,Imie,Nazwisko,DataUrodzenia,Nacjonalnosc,Wiek,Plec,Trener "
                                                           + "from zawodnik z inner join gosc g on z.IdZawodnik = g.IdGosc "
                                                           + "where g.IdGosc = @idGosc ", connection))
                {
                    command.Parameters.AddWithValue("idGosc", id);

                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                zawodnik.IdGosc = Convert.ToInt32(reader[0]);
                                zawodnik.Imie = reader[1].ToString();
                                zawodnik.Nazwisko = reader[2].ToString();
                                zawodnik.DataUrodzenia = Convert.ToDateTime(reader[3]);
                                zawodnik.Kraj = reader[4].ToString();
                                zawodnik.Wiek = Convert.ToInt32(reader[5]);
                                zawodnik.Plec = reader[6].ToString();
                                zawodnik.Trener = reader[7] == DBNull.Value ? 0 : Convert.ToInt32(reader[7]);
                            }
                        }
                    }
                    catch (SqlException) { Console.WriteLine("SQL EXCEPTION"); }//exception
                }
                return zawodnik;
            }
        }

        public bool deleteZawodnik(int? id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("delete from zawodnik where idzawodnik = @idzawodnik", connection))
                    {
                        command.Parameters.AddWithValue("idzawodnik", id);

                        int affectedRows = command.ExecuteNonQuery();
                        Console.WriteLine(affectedRows);
                    }
                }
                return true;
            }
            catch (SqlException) { return false; }
        }
       
        public List<Zawodnik> getZawodnikList()
        {
            List<Zawodnik> lista = new List<Zawodnik>();

            using (SqlConnection connect = new SqlConnection(constring))
            {
                connect.Open();

                string query = "select imie, nazwisko, dataurodzenia, nacjonalnosc, wiek, plec, g.idgosc from zawodnik z inner join gosc g on z.IdZawodnik = g.IdGosc";

                using (SqlCommand command = new SqlCommand(query, connect))
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        lista.Add(new Zawodnik
                        {
                            Imie = reader[0].ToString(),
                            Nazwisko = reader[1].ToString(),
                            DataUrodzenia = Convert.ToDateTime(reader[2]),
                            Kraj = reader[3].ToString(),
                            Wiek = Convert.ToInt32(reader[4]),
                            Plec = reader[5].ToString(),
                            IdGosc = Convert.ToInt32(reader[6])
                        });
                    }
            }
            return lista;
        }

        internal List<ZawodnikDetails> getDetailsList(int? id)
        {
            List<ZawodnikDetails> lista = new List<ZawodnikDetails>();

            using (SqlConnection connection = new SqlConnection(constring))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("select m.Data,g1.Imie+' '+g1.Nazwisko as 'Zawodnik 1',g2.Imie+' '+g2.Nazwisko as 'Zawodnik 2',m.Wynik1,m.Wynik2 from mecz m "
                                                           + "inner join Zawodnik z1 on m.IdZawodnik1=z1.IdZawodnik "
                                                           + "inner join Gosc	g2 on m.IdZawodnik2=g2.IdGosc "
                                                           + "inner join Gosc g1 on g1.IdGosc=z1.IdZawodnik "
                                                           + "where m.IdZawodnik1=@idzawod  order by m.Data desc ", connection))
                {
                    command.Parameters.AddWithValue("idzawod", id);

                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new ZawodnikDetails
                                {
                                    Data = Convert.ToDateTime(reader[0]),
                                    Zawodnik1 = reader[1].ToString(),
                                    Zawodnik2 = reader[2].ToString(),
                                    Wynik1 = Convert.ToInt16(reader[3]),
                                    Wynik2 = Convert.ToInt16(reader[4])
                                });
                            }
                        }
                    }
                    catch (SqlException) { }//exception
                }
                return lista;
            }
        }

        internal bool deleteZawodnikWithMecz(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("exec TINPROdeleteZawodnikWithMecz @idzawodnik", connection))
                    {
                        command.Parameters.AddWithValue("idzawodnik", id);

                        int affectedRows = command.ExecuteNonQuery();
                        Console.WriteLine(affectedRows);
                    }
                }
                return true;
            }
            catch (SqlException) { return false; }
        }

        // <<------------------------------------ TABELA MECZ ----------------------------->>
        internal bool updateMecz(Mecz m, int sedzia)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("exec TINPROupdateMecz @id,@data,@wynik1,@wynik2,@idz1,@idz2,@idsedzia", connection))
                    {
                        command.Parameters.AddWithValue("id", m.IdMecz);
                        command.Parameters.AddWithValue("data", m.Data);
                        command.Parameters.AddWithValue("wynik1", m.Wynik1);
                        command.Parameters.AddWithValue("wynik2", m.Wynik2);
                        command.Parameters.AddWithValue("idz1", m.Zawodnik1);
                        command.Parameters.AddWithValue("idz2", m.Zawodnik2);
                        command.Parameters.AddWithValue("idsedzia", sedzia);

                        int affectedRows = command.ExecuteNonQuery();
                        // Console.WriteLine(affectedRows);
                    }

                    return true;
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public List<MeczListOpak> getMeczList(){
            List<MeczListOpak> lista = new List<MeczListOpak>();

            using (SqlConnection connect = new SqlConnection("Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True"))
            {
                connect.Open();

                string query = "select m.IdMeczu,m.data,g.Imie + ' ' + g.Nazwisko as Zawodnik1,g1.Imie + ' ' + g1.Nazwisko as Zawodnik2,m.Wynik1,m.Wynik2,g.IdGosc,g1.IdGosc from mecz m inner join gosc g on m.IdZawodnik1 = g.IdGosc inner join gosc g1 on m.IdZawodnik2 = g1.IdGosc";

                try
                {
                    using (SqlCommand command = new SqlCommand(query, connect))
                    using (SqlDataReader reader = command.ExecuteReader())
                        while (reader.Read())
                        {
                            lista.Add(new MeczListOpak
                            {
                                IdMecz = Convert.ToInt32(reader[0]),
                                Data = Convert.ToDateTime(reader[1]),
                                Zawodnik1Imie = reader[2].ToString(),
                                Zawodnik2Imie = reader[3].ToString(),
                                Wynik1 = Convert.ToInt32(reader[4]),
                                Wynik2 = Convert.ToInt32(reader[5]),
                                Zawodnik1 = Convert.ToInt32(reader[6]),
                                Zawodnik2 = Convert.ToInt32(reader[7])
                            });
                        }
                }
                catch (SqlException) { return lista; }
            }
            return lista;
        }

        public bool insertMecz(Mecz m,int sedzia){
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();
                    //exec TINPROaddNewMecz '2019-11-18','5','6','2','5';

                    using (SqlCommand command = new SqlCommand("exec TINPROaddNewMecz @data,@wynik1,@wynik2,@idzawod1,@idzawod2,@idsedzia", connection))
                    {
                        command.Parameters.AddWithValue("data", m.Data);
                        command.Parameters.AddWithValue("wynik1", m.Wynik1);
                        command.Parameters.AddWithValue("wynik2", m.Wynik2);
                        command.Parameters.AddWithValue("idzawod1", m.Zawodnik1);
                        command.Parameters.AddWithValue("idzawod2", m.Zawodnik2);
                        command.Parameters.AddWithValue("idsedzia", sedzia);

                        int affectedRows = command.ExecuteNonQuery();
                        //Console.Write(affectedRows);   
                    }
                }
            }catch (SqlException){return false;}
            return true;
        }

        internal bool deleteMecz(int? id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("exec TINPROdeleteMecz  @idmecz", connection))
                    {
                        command.Parameters.AddWithValue("idmecz", id);

                        int affectedRows = command.ExecuteNonQuery();
                        //  Console.WriteLine(affectedRows);
                    }
                }
                return true;
            }
            catch (SqlException) { return false; }
        }

        public Mecz getMeczById(int? id)
        {
            Mecz mecz = new Mecz();

            using (SqlConnection connection = new SqlConnection(constring))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("select * from mecz where idmeczu=@idmecz", connection))
                {
                    command.Parameters.AddWithValue("idmecz", id);

                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                mecz.IdMecz = Convert.ToInt32(reader[0]);
                                mecz.Data = Convert.ToDateTime(reader[1]);
                                mecz.Wynik1 = Convert.ToInt32(reader[2]);
                                mecz.Wynik2 = Convert.ToInt32(reader[3]);
                                mecz.Zawodnik1 = Convert.ToInt32(reader[4]);
                                mecz.Zawodnik2 = Convert.ToInt32(reader[5]);
                            }
                        }
                    }
                    catch (SqlException) { return null; }//exception
                }
                return mecz;
            }
        }

        // <<------------------------------------ TABELA SEDZIA ----------------------------->>
        internal dynamic getSedziaList()
        {
            List<Sedzia> lista = new List<Sedzia>();

            using (SqlConnection connect = new SqlConnection(constring))
            {
                connect.Open();

                string query = "select idgosc,imie,nazwisko,dataurodzenia from gosc g inner join sedzia s on s.IdSedzia=g.IdGosc ";

                using (SqlCommand command = new SqlCommand(query, connect))
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        lista.Add(new Sedzia
                        {
                            IdSedzia=Convert.ToInt32(reader[0]),
                            Imie=reader[1].ToString(),
                            Nazwisko=reader[2].ToString(),
                            DataUrodzenia = Convert.ToDateTime(reader[3])
                        });
                    }
            }
            return lista;
        }

        internal bool insertSedzia(Zawodnik z)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();
                    //exec TINPROaddSedzia 'imie','nazwisko','2019-12-18';

                    using (SqlCommand command = new SqlCommand("exec TINPROaddSedzia @imie, @nazwisko, @data;", connection))
                    {
                        command.Parameters.AddWithValue("imie", z.Imie);
                        command.Parameters.AddWithValue("nazwisko", z.Nazwisko);
                        command.Parameters.AddWithValue("data", z.DataUrodzenia);

                        int affectedRows = command.ExecuteNonQuery();
                        //Console.Write(affectedRows);   
                    }
                }
            }
            catch (SqlException) { return false; }
            return true;
        }

        internal bool updateSedzia(Sedzia z)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("update gosc set imie=@imie, Nazwisko=@nazwisko, DataUrodzenia=@data where idgosc=@id", connection))
                    {
                        command.Parameters.AddWithValue("imie", z.Imie);
                        command.Parameters.AddWithValue("nazwisko", z.Nazwisko);
                        command.Parameters.AddWithValue("data", z.DataUrodzenia);
                        command.Parameters.AddWithValue("id", z.IdSedzia);

                        int affectedRows = command.ExecuteNonQuery();
                        //Console.Write(affectedRows);   
                    }
                }
            }
            catch (SqlException) { return false; }
            return true;
        }

        internal bool deleteSedzia(int idSedzia)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("begin transaction; delete from sedziaNaMeczu where IdSedzia=@id; delete from sedzia where IdSedzia=@id; commit; ", connection))
                    {
                        command.Parameters.AddWithValue("id", idSedzia);

                        int affectedRows = command.ExecuteNonQuery();
                        //Console.Write(affectedRows);   
                    }
                }
            }
            catch (SqlException) { return false; }
            return true;
        }

        internal Sedzia getSedziaByMecz(int? idMecz)
        {
            Sedzia sedz = new Sedzia();

            using (SqlConnection connection = new SqlConnection(constring))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("select sd.IdSedzia, g.imie, g.nazwisko from gosc g "
                                                           + "inner join sedzia sd on g.IdGosc=sd.IdSedzia "
                                                           + "inner join sedziaNaMeczu snm on snm.IdSedzia=sd.IdSedzia "
                                                           + "where snm.IdMeczu=@idMecz", connection))
                {
                    command.Parameters.AddWithValue("idMecz", idMecz);

                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                sedz.IdSedzia = Convert.ToInt32(reader[0]);
                                sedz.Imie = reader[1].ToString();
                                sedz.Nazwisko = reader[2].ToString();
                            }
                        }
                    }
                    catch (SqlException) { Console.WriteLine("SQL EXCEPTION"); }//exception
                }
                return sedz;
            }
        }

        internal List<MeczListOpak> getSedziaDetails(int? id)
        {
            List<MeczListOpak> lista = new List<MeczListOpak>();

            using (SqlConnection connect = new SqlConnection("Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True"))
            {
                connect.Open();

                string query = "select m.data,z1.Imie+z1.Nazwisko as 'Zawodnik 1',z2.Imie+z2.Nazwisko as 'Zawodnik 2' from mecz m "
                              + "inner join gosc z1 on m.IdZawodnik1=z1.IdGosc "
                              + "inner join gosc z2 on m.IdZawodnik2=z2.IdGosc "
                              + "inner join sedziaNaMeczu snm on m.IdMeczu=snm.IdMeczu "
                              + "inner join sedzia s on s.IdSedzia=snm.IdSedzia where snm.IdSedzia=@id";

                try
                {
                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("id", id);
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                        while (reader.Read())
                        {
                            lista.Add(new MeczListOpak
                            {
                                Data = Convert.ToDateTime(reader[0]),
                                Zawodnik1Imie = reader[1].ToString(),
                                Zawodnik2Imie = reader[2].ToString()
                            });
                        }
                    }
                }
                catch (SqlException) { return lista; }
            }
            return lista;
        }

        // <<------------------------------------ TABELA GOSC ----------------------------->>
        internal bool insertGosc(Zawodnik z)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();
                    //exec TINPROaddNewMecz '2019-11-18','5','6','2','5';

                    using (SqlCommand command = new SqlCommand("exec TINPROaddGosc @imie, @nazwisko, @data;", connection))
                    {
                        command.Parameters.AddWithValue("imie", z.Imie);
                        command.Parameters.AddWithValue("nazwisko", z.Nazwisko);
                        command.Parameters.AddWithValue("data", z.DataUrodzenia);


                        int affectedRows = command.ExecuteNonQuery();
                        //Console.Write(affectedRows);   
                    }
                }
            }
            catch (SqlException) { return false; }
            return true;
        }

        internal List<Zawodnik> getGosciList()
        {
            List<Zawodnik> lista = new List<Zawodnik>();

            using (SqlConnection connect = new SqlConnection(constring))
            {
                connect.Open();

                string query = "select Idgosc,Imie,Nazwisko,DataUrodzenia from gosc";

                using (SqlCommand command = new SqlCommand(query, connect))
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        lista.Add(new Zawodnik
                        {
                            IdGosc = Convert.ToInt32(reader[0]),
                            Imie = reader[1].ToString(),
                            Nazwisko = reader[2].ToString(),
                            DataUrodzenia = Convert.ToDateTime(reader[3])
                        });
                    }
            }
            return lista;
        }

        internal Zawodnik getGoscById(int idgosc)
        {
            Zawodnik gosc = new Zawodnik();

            using (SqlConnection connection = new SqlConnection(constring))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("select Imie,Nazwisko from gosc where idgosc=@idgosc", connection))
                {
                    command.Parameters.AddWithValue("idgosc", idgosc);//id

                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                gosc.IdGosc = idgosc;
                                gosc.Imie = reader[0].ToString();
                                gosc.Nazwisko = reader[1].ToString();
                            }
                        }
                    }
                    catch (SqlException) { Console.WriteLine("SQL EXCEPTION"); }//exception
                }
                return gosc;
            }
        }

        // <<------------------------------------ TABELA KIBIC ----------------------------->>

        internal List<Zawodnik> getKibicList()
        {
            List<Zawodnik> data = new List<Zawodnik>();

            using (SqlConnection connection = new SqlConnection(constring))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("select IdGosc,Imie,Nazwisko from gosc g inner join kibic c  on g.IdGosc=c.IdKibic ", connection))
                {

                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                data.Add(new Zawodnik
                                {
                                    IdGosc = Convert.ToInt32(reader[0]),
                                    Imie = reader[1].ToString(),
                                    Nazwisko = reader[2].ToString()
                                });
                            }
                        }
                    }
                    catch (SqlException) { Console.WriteLine("SQL EXCEPTION"); }//exception
                }
                return data;
            }
        }

        internal List<Zawodnik> getKibicList(int? idMecz)
        {
            List<Zawodnik> data = new List<Zawodnik>();

            using (SqlConnection connection = new SqlConnection(constring))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("select knm.IdMeczu,g.IdGosc,g.imie,g.nazwisko from KibicNaMiejscu knm "
                                                           + "inner join gosc g on g.IdGosc=knm.IdKibic where knm.idmeczu=@idMecz", connection))
                {
                    command.Parameters.AddWithValue("idMecz", idMecz);

                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                data.Add(new Zawodnik
                                {
                                    IdGosc = Convert.ToInt32(reader[1]),
                                    Imie = reader[2].ToString(),
                                    Nazwisko = reader[3].ToString()
                                });
                            }
                        }
                    }
                    catch (SqlException) { Console.WriteLine("SQL EXCEPTION"); }//exception
                }
                return data;
            }
        }

        internal bool updateKibic(Zawodnik z)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("update gosc set imie=@imie, Nazwisko=@nazwisko, DataUrodzenia=@data where idgosc=@id", connection))
                    {
                        command.Parameters.AddWithValue("imie", z.Imie);
                        command.Parameters.AddWithValue("nazwisko", z.Nazwisko);
                        command.Parameters.AddWithValue("data", z.DataUrodzenia);
                        command.Parameters.AddWithValue("id", z.IdGosc);

                        int affectedRows = command.ExecuteNonQuery();
                        //Console.Write(affectedRows);   
                    }
                }
            }
            catch (SqlException) { return false; }
            return true;
        }

        internal bool deleteKibic(int idSedzia)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("begin transaction; delete from KibicNaMiejscu where IdKibic=@id; delete from kibic where IdKibic=@id; delete from gosc where IdGosc=@id; commit;", connection))
                    {
                        command.Parameters.AddWithValue("id", idSedzia);

                        int affectedRows = command.ExecuteNonQuery();
                        //Console.Write(affectedRows);   
                    }
                }
            }
            catch (SqlException) { return false; }
            return true;
        }

        internal List<KibicDetails> getDetailsKibic(int? id)
        {
            List<KibicDetails> lista = new List<KibicDetails>();

            using (SqlConnection connection = new SqlConnection(constring))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("select m.data,z1.Imie+z1.Nazwisko as 'Zawodnik 1',z2.Imie+z2.Nazwisko as 'Zawodnik 2', me.NumerMiejsca from mecz m "
                                                           + "inner join gosc z1 on m.IdZawodnik1=z1.IdGosc "
                                                           + "inner join gosc z2 on m.IdZawodnik2=z2.IdGosc "
                                                           + "inner join KibicNaMiejscu knm on m.IdMeczu=knm.IdMeczu "
                                                           + "inner join Miejsce me on knm.IdMiejsca=me.IdMiejsca where knm.IdKibic=@idzawod ", connection))
                {
                    command.Parameters.AddWithValue("idzawod", id);

                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new KibicDetails
                                {
                                    DataMeczu = Convert.ToDateTime(reader[0]),
                                    Zawodnik1 = reader[1].ToString(),
                                    Zawodnik2 = reader[2].ToString(),
                                    NumerMiejsca = Convert.ToInt32(reader[3])
                                });
                            }
                        }
                    }
                    catch (SqlException) { }//exception
                }
                return lista;
            }
        }


        // <<------------------------------------ TABELA KibicNaMiejsce ----------------------------->>
        internal List<Miejsce> getMiejscaList(int idMecz)
        {
            List<Miejsce> lista = new List<Miejsce>();

            using (SqlConnection connection = new SqlConnection(constring))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("select m.IdMiejsca,m.numerMiejsca,m.VIP from Miejsce m  where IdMiejsca!=all(select IdMiejsca from KibicNaMiejscu where IdMeczu=@IdMeczu)", connection))
                {
                    command.Parameters.AddWithValue("IdMeczu", idMecz);

                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Miejsce
                                {
                                    IdMiejsca = Convert.ToInt32(reader[0]),
                                    NumerMiejsca = Convert.ToInt32(reader[1]),
                                    VIP = reader[2].ToString()
                                });
                            }
                        }
                    }
                    catch (SqlException) { return null; }//exception
                }
                return lista;
            }
        }

        internal bool insertMiejsce(int idMiejsca, int idgosc, int idmecz)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();
                   
                    using (SqlCommand command = new SqlCommand("insert into KibicNaMiejscu(IdMeczu,IdMiejsca,IdKibic) values(@idMeczu,@IdMiejsca,@IdKibic)", connection))
                    {
                        command.Parameters.AddWithValue("idMeczu", idmecz);
                        command.Parameters.AddWithValue("IdMiejsca", idMiejsca);
                        command.Parameters.AddWithValue("IdKibic", idgosc);

                        int affectedRows = command.ExecuteNonQuery();  
                    }
                }
            }
            catch (SqlException) { return false; }
            return true;
        }

        internal Miejsce getMiesceById(int idMiejsca)
        {
            Miejsce mm = new Miejsce();

            using (SqlConnection connection = new SqlConnection(constring))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("select * from Miejsce where IdMiejsca=@idMiejsca", connection))
                {
                    command.Parameters.AddWithValue("idMiejsca", idMiejsca);

                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                mm.IdMiejsca = Convert.ToInt32(reader[0]);
                                mm.NumerMiejsca = Convert.ToInt32(reader[1]);
                                mm.VIP = reader[2].ToString();
                            }
                        }
                    }
                    catch (SqlException) { Console.WriteLine("SQL EXCEPTION"); }//exception
                }
                return mm;
            }
        }

        internal bool deleteMiejsce(int idGosc, int idMecz,int numerMiejsca)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("delete from KibicNaMiejscu where IdKibic = @idkibic and IdMeczu = @idmecz and IdMiejsca = (select IdMiejsca from Miejsce where NumerMiejsca=@idmiejsce)", connection))
                    {
                        command.Parameters.AddWithValue("idkibic",idGosc);
                        command.Parameters.AddWithValue("idmecz", idMecz);
                        command.Parameters.AddWithValue("idmiejsce", numerMiejsca);

                        int affectedRows = command.ExecuteNonQuery();
                        //  Console.WriteLine(affectedRows);
                    }
                }
                return true;
            }
            catch (SqlException) { return false; }
        }

        internal List<organiseDetails> getOrganiseDetailsList(int idMecz)
        {
            List<organiseDetails> lista = new List<organiseDetails>();

            using (SqlConnection connection = new SqlConnection(constring))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("select	g.IdGosc,g.Imie,g.Nazwisko,knm.IdMeczu,m.NumerMiejsca from KibicNaMiejscu knm " +
                                                           "inner join gosc g on knm.IdKibic=g.IdGosc "+
                                                           "inner join Miejsce m on m.IdMiejsca=knm.IdMiejsca "+
                                                           "where knm.IdMeczu=@IdMeczu", connection))
                {
                    command.Parameters.AddWithValue("IdMeczu", idMecz);

                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new organiseDetails
                                {
                                    IdGosc = Convert.ToInt32(reader[0]),
                                    Imie = reader[1].ToString(),
                                    Nazwisko = reader[2].ToString(),
                                    IdMeczu = Convert.ToInt32(reader[3]),
                                    NumeMiejsca = Convert.ToInt32(reader[4])
                                }) ;
                            }
                        }
                    }
                    catch (SqlException) { return null; }//exception
                }
                return lista;
            }
        }
    }
}
