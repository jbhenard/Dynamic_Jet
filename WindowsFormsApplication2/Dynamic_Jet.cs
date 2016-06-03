using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Accès DB
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Base_Dynamic_Jet{
    class Client{
        //Connection
        public SqlConnection connexion = new SqlConnection();
        public SqlCommand commande = new SqlCommand();
        private string requete = "SELECT * FROM Client";
        private string choix; //"Ajouter", "Modifier", "Tous", "FD" & "Supprimer"

        //Attributs Client
        private int id_client; //int
        private string nom; //nchar(15)
        private string prenom; //nchar(15)
        private string adresse; //nchar(50)
        private string telephone; //nchar(20)
        private DateTime date_naissance; //date
        private string numero_permis; //nchar(50)
        private string mail; //nchar(30)
        private string ChCritères;

        //En premier lieu, il faut instancier un objet SDA d’un type dérivé de la classe DbDataAdapter.
        public SqlDataAdapter sda = new SqlDataAdapter(); //SDA = sda

        //DataSet pour le Formulaire à venir
        public DataSet DsDonnée = new DataSet(); //DataSet1
        public DataTable DtDonnée = new DataTable();  //DataSet2

        //Méthode de connection
        //public object ConnectionState { get; private set; }
        public ConnectionState State{
            get { return commande.Connection.State; }
        }

        public void SetChCritères(string ChCritères)
        {
            this.ChCritères = ChCritères;
        }
        public void SetId_client(int id_client)
        {
            this.id_client = id_client;
        }

        public void SetNom(string param_nom)
        {
            this.nom = param_nom;
        }

        public void SetPrenom(string param_prenom)
        {
            this.prenom = param_prenom;
        }


        public void SetAdresse(string param_adresse)
        {
            this.adresse = param_adresse;
        }

        public void SetTelephone(string param_telephone)
        {
            this.telephone = param_telephone;
        }

        public void SetDate_naissance(DateTime param_date_naissance)
        {
            this.date_naissance = param_date_naissance;
        }

        public void SetNumero_permis(string param_numero_permis)
        {
            this.numero_permis = param_numero_permis;
        }

        public void SetMail(string param_mail)
        {
            this.mail = param_mail;
        }

        //Teste l'état de la connection
        public void connecter(){
            //nom db, nom serveur, mode sécurité
            connexion.ConnectionString = "Initial catalog='Dynamic_Jet' ; data source = 'CTI-GEN-008' ; integrated security=true";

            //connexion.ConnectionString = "Initial catalog='Dynamic_Jet' ; data source = 'JBH" + "\\"+"SQLEXPRESS' ; integrated security=true";
            //JBH\SQLEXPRESS
            connexion.Open();
        }

        //Génère une requête SQL en fonction du code paramètre nommé 'choix'
        public string Requete(string choix){
            this.choix = choix;
            switch (choix)
            {
                case "Tous":
                    requete = "SELECT * FROM Client";
                    break;
                case "FD":                    
                    requete = "SELECT * FROM Client WHERE id_client = " + this.id_client;
                    break;
                case "Ajouter":                    
                    requete = "INSERT INTO Client (id_client, nom, prenom, adresse, telephone, date_naissance, numero_permis, mail) VALUES (" +
                              this.id_client + ", '" + this.nom + "', '" + this.prenom + "', '" + this.adresse + "', '" + this.telephone + "', '" + this.date_naissance+ "', '" + this.numero_permis + "', '" + this.mail + "')";
                    //MessageBox.Show("Chaine SQL= " + requete, "Infos Insert", MessageBoxButtons.OK);
                    break;
                case "Supprimer":                    
                    requete = "DELETE FROM Client WHERE id_client = " + this.id_client; 
                    break;
                case "Modifier":                    
                    requete = "UPDATE Client SET id_client = " + this.id_client + ", nom = '" + this.nom + "', prenom = '" + this.prenom + "', adresse = '" + this.adresse  +
                            "', telephone = '" + this.telephone + "', date_naissance = '" + this.date_naissance + "', numero_permis = '" + this.numero_permis + "', mail = '" + this.mail +
                           "' WHERE id_client = " + this.id_client;
                    break;
                
                case "Doublon":
                    requete = "SELECT COUNT(*) as 'Cltexiste' FROM Client WHERE " + this.ChCritères;
                    break;

                case "RésaClt":
                    requete = "SELECT A.* FROM Reservation A WHERE [id_client] = " + this.id_client;
                    break;

                default:
                    Console.WriteLine("Erreur de choix!");
                    break;
            }            
            return requete;
        }

        //Lance la requête en SqlDataReader = mode console
        public SqlDataReader Afficher1(string requeteL){
            connecter(); //Appel méthode d econnexion
            commande.Connection = connexion; //Définit au début de la class
            commande.CommandText = requeteL; //Paramètre
            SqlDataReader reader = commande.ExecuteReader(); //Lance la requête
            return reader;
        }

        //Lance la requête en SqlDataAdapter = mode formulaire
        public SqlDataAdapter AfficherTous(string requeteL)
        {
            connecter(); //Appel méthode de connexion
            commande.Connection = connexion; //Définit au début de la class

            switch (this.choix)
            {
                case "Tous":
                case "FD":
                case "Doublon":
                case "RésaClt":
                    commande.CommandText = requeteL; //Paramètre        
                    sda.SelectCommand = commande;    //Lancer la requête            
                    //return sda;                                    
                    break;

                case "Ajouter":
                    try
                    {
                        sda.InsertCommand = new SqlCommand(requeteL, connexion);
                        sda.InsertCommand.ExecuteNonQuery();
                        MessageBox.Show("Enregistrement Client créé n°" + this.id_client, "Ajout OK", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;

                case "Supprimer":
                    try
                    {
                        sda.DeleteCommand = new SqlCommand(requeteL, connexion);
                        sda.DeleteCommand.ExecuteNonQuery();
                        MessageBox.Show("Enregistrement Client supprimé n°" + this.id_client, "suppression OK", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;

                case "Modifier":
                    try
                    {
                        sda.UpdateCommand = new SqlCommand(requeteL, connexion);
                        sda.UpdateCommand.ExecuteNonQuery();
                        MessageBox.Show("Enregistrement Client Modifié n°" + this.id_client, "MàJ OK", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;

                default:
                    Console.WriteLine("Erreur de choix!");
                    break;
            } //fin switch    
            return sda;
        } //Fin méthode


        //Libérée l'accès à la DB
        public void deconnecter(){
            connexion.Close();
        }

    } //Fin Class Client



    //***********************************************************
    //Debut class employé

    class Employé
    {
        //Connection
        public SqlConnection connexion = new SqlConnection();
        public SqlCommand commande = new SqlCommand();
        private string requete = "SELECT * FROM Employe";
        private string choix; //"Ajouter", "Modifier", "Tous", "FD" & "Supprimer"

        //Attributs Employé
        private string numero_secu; //nchar(15)
        private DateTime date_visite_medicale; //date
        private string type_contrat; //nchar(10)
        private string numero_permis; //nchar(50)
        private string statut_activite; //nchar(10)
        private string nom; //nchar(10)
        private string prenom; //nchar(10)
        private DateTime date_embauche; //date

        //En premier lieu, il faut instancier un objet SDA d’un type dérivé de la classe DbDataAdapter.
        public SqlDataAdapter sda = new SqlDataAdapter(); //SDA = sda

        //DataSet pour le Formulaire à venir
        public DataSet DsDonnée = new DataSet(); //DataSet1
        public DataTable DtDonnée = new DataTable();  //DataSet2

        //Méthode de connection
        //public object ConnectionState { get; private set; }
        public ConnectionState State
        {
            get { return commande.Connection.State; }
        }

        public void SetNumero_secu(string param_id)
        {
            this.numero_secu = param_id;
        }
     
        public void SetDate_visite_medicale(DateTime date_visite_medicale)
        {
            this.date_visite_medicale = date_visite_medicale;
        }

        public void SetType_contrat(string type_contrat)
        {
            this.type_contrat = type_contrat;
        }

        public void SetNumero_permis(string param_numero_permis)
        {
            this.numero_permis = param_numero_permis;
        }

        public void SetStatut_activite(string statut_activite)
        {
            this.statut_activite = statut_activite;
        }

        public void SetNom(string param_nom)
        {
            this.nom = param_nom;
        }

        public void SetPrenom(string param_prenom)
        {
            this.prenom = param_prenom;
        }

        public void SetDate_embauche(DateTime date_embauche)
        {
            this.date_embauche = date_embauche;
        }

        //Teste l'état de la connection
        public void connecter()
        {
            //nom db, nom serveur, mode sécurité
            connexion.ConnectionString = "Initial catalog='Dynamic_Jet' ; data source = 'CTI-GEN-008' ; integrated security=true";

            //connexion.ConnectionString = "Initial catalog='Dynamic_Jet' ; data source = 'JBH" + "\\" + "SQLEXPRESS' ; integrated security=true";
            //JBH\SQLEXPRESS
            connexion.Open();
        }

        //Génère une requête SQL
        public string Requete(string choix)
        {
            this.choix = choix;
            switch (choix)
            {
                case "Tous":
                    requete = "SELECT * FROM Employe";
                    break;
                case "FD":
                    requete = "SELECT * FROM Employe WHERE numero_secu = " + this.numero_secu;
                    break;
                case "Ajouter":
                    requete = "INSERT INTO Employe (numero_secu, date_visite_medicale, type_contrat, numero_permis, statut_activite, nom, prenom, date_embauche) VALUES (" +
                              this.numero_secu + ", '" + this.date_visite_medicale + "', '" + this.type_contrat + "', '" + this.numero_permis + "', '" + this.statut_activite + "', '" +
                              this.nom + "', '" + this.prenom + "', '" + this.date_embauche + "')";
                    //MessageBox.Show("Chaine SQL= " + requete, "Infos Insert", MessageBoxButtons.OK);
                    break;
                case "Supprimer":
                    requete = "DELETE FROM Employe WHERE numero_secu = " + this.numero_secu;
                    break;
                case "Modifier":
                    requete = "UPDATE Employe SET numero_secu = " + this.numero_secu + ", date_visite_medicale = '" + this.date_visite_medicale + "', type_contrat = '" + 
                              this.type_contrat + "', numero_permis = '" + this.numero_permis +
                              "', statut_activite = '" + this.statut_activite + "', nom = '" + this.nom + "', prenom = '" + this.prenom +
                              "', date_embauche = '" + this.date_embauche +
                              "' WHERE numero_secu = " + this.numero_secu;
                    break;

                case "AbsencesEmployé":
                    requete = "SELECT * FROM Absence WHERE id_employe = " + this.numero_secu;
                    break;

                default:
                    Console.WriteLine("Erreur de choix!");
                    break;
            } //Fin de switch          
            return requete;
        }

        //Lance la requête en SqlDataReader = mode console
        public SqlDataReader Afficher1(string requeteL)
        {
            connecter(); //Appel méthode d econnexion
            commande.Connection = connexion; //Définit au début de la class
            commande.CommandText = requeteL; //Paramètre
            SqlDataReader reader = commande.ExecuteReader(); //Lance la requête
            return reader;
        }

        //Lance la requête en SqlDataAdapter = mode formulaire
        public SqlDataAdapter AfficherTous(string requeteL)
        {
            connecter(); //Appel méthode de connexion
            commande.Connection = connexion; //Définit au début de la class

            switch (this.choix)
            {
                case "Tous":
                case "FD":
                case "AbsencesEmployé":
                    commande.CommandText = requeteL; //Paramètre        
                    sda.SelectCommand = commande;    //Lancer la requête            
                    //return sda;                                    
                    break;

                case "Ajouter":
                    try
                    {
                        sda.InsertCommand = new SqlCommand(requeteL, connexion);
                        sda.InsertCommand.ExecuteNonQuery();
                        MessageBox.Show("Enregistrement Employé créé n°" + this.numero_secu, "Ajout OK", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;

                case "Supprimer":
                    try
                    {
                        sda.DeleteCommand = new SqlCommand(requeteL, connexion);
                        sda.DeleteCommand.ExecuteNonQuery();
                        MessageBox.Show("Enregistrement Employé supprimé n°" + this.numero_secu, "suppression OK", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;

                case "Modifier":
                    try
                    {
                        sda.UpdateCommand = new SqlCommand(requeteL, connexion);
                        sda.UpdateCommand.ExecuteNonQuery();
                        MessageBox.Show("Enregistrement Employé Modifié n°" + this.numero_secu, "MàJ OK", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;

                default:
                    Console.WriteLine("Erreur de choix!");
                    break;
            } //Fin switch    
            return sda;
        } //Fin méthode


        //Libérée l'accès à la DB
        public void deconnecter()
        {
            connexion.Close();
        }

    }

    //Fin class employé



    //***********************************************************
    //Debut class Absence

    class Absence
    {
        //Connection
        public SqlConnection connexion = new SqlConnection();
        public SqlCommand commande = new SqlCommand();
        private string requete = "SELECT * FROM Absence";
        private string choix; //"Ajouter", "Modifier", "Tous", "FD" & "Supprimer"

        //Attributs Employé
        private string id_employe; //nchar(15)
        private DateTime date_debut; //date
        private DateTime date_fin; //date       
        private string id_absence; //nchar(10)
        private string motif; //nchar(50)
        private DateTime heure_debut;
        private DateTime heure_fin;

        //En premier lieu, il faut instancier un objet SDA d’un type dérivé de la classe DbDataAdapter.
        public SqlDataAdapter sda = new SqlDataAdapter(); //SDA = sda

        //DataSet pour le Formulaire à venir
        public DataSet DsDonnée = new DataSet(); //DataSet1
        public DataTable DtDonnée = new DataTable();  //DataSet2

        //Méthode de connection
        //public object ConnectionState { get; private set; }
        public ConnectionState State
        {
            get { return commande.Connection.State; }
        }

        public void SetId_employe(string id_employe)
        {
            this.id_employe = id_employe;
        }

        public void SetDate_debut(DateTime date_debut)
        {
            this.date_debut = date_debut;
        }

        public void SetDate_fin(DateTime date_fin)
        {
            this.date_fin = date_fin;
        }

        public void SetId_absence(string id_absence)
        {
            this.id_absence = id_absence;
        }

        public void SetMotif(string motif)
        {
            this.motif = motif;
        }

        public void SetHeure_debut(DateTime heure_debut)
        {
            this.heure_debut = heure_debut;
        }

        public void SetHeure_fin(DateTime heure_fin)
        {
            this.heure_fin = heure_fin;
        }
        //Teste l'état de la connection
        public void connecter()
        {
            //nom db, nom serveur, mode sécurité
            connexion.ConnectionString = "Initial catalog='Dynamic_Jet' ; data source = 'CTI-GEN-008' ; integrated security=true";

            //connexion.ConnectionString = "Initial catalog='Dynamic_Jet' ; data source = 'JBH" + "\\" + "SQLEXPRESS' ; integrated security=true";
            //JBH\SQLEXPRESS
            connexion.Open();
        }

        //Génère une requête SQL
        public string Requete(string choix)
        {
            this.choix = choix;
            switch (choix)
            {
                case "Tous":
                    requete = "SELECT * FROM Absence";
                    break;
                case "FD":
                    requete = "SELECT * FROM Absence WHERE id_absence = " + this.id_absence;
                    break;
                case "Ajouter":
                    requete = "INSERT INTO Absence (id_employe, date_debut, date_fin, id_absence, motif, heure_debut, heure_fin) VALUES (" +
                              this.id_employe + ", '" + this.date_debut + "', '" + this.date_fin + "', " + this.id_absence + ", '" + this.motif + "', '" +
                              this.heure_debut +"', '" + this.heure_fin + "')";
                    //MessageBox.Show("Chaine SQL= " + requete, "Infos Insert", MessageBoxButtons.OK);
                    break;
                case "Supprimer":
                    requete = "DELETE FROM Absence WHERE id_absence = " + this.id_absence;
                    break;
                case "Modifier":
                    requete = "UPDATE Absence SET id_employe = " + this.id_employe + ", date_debut = '" + this.date_debut + "', date_fin = '" +
                              this.date_fin + "', id_absence = " + this.id_absence +
                              ", motif = '" + this.motif + "', heure_debut = '" +  this.heure_debut + "', heure_fin = '"  +  this.heure_fin +
                              "' WHERE id_absence = " + this.id_absence;
                    break;

                case "MaxAbsence":
                    requete = "SELECT max(A.id_absence) FROM Absence A";
                    break;

                default:
                    Console.WriteLine("Erreur de choix!");
                    break;
            } //Fin de switch          
            return requete;
        }

        //Lance la requête en SqlDataReader = mode console
        public SqlDataReader Afficher1(string requeteL)
        {
            connecter(); //Appel méthode d econnexion
            commande.Connection = connexion; //Définit au début de la class
            commande.CommandText = requeteL; //Paramètre
            SqlDataReader reader = commande.ExecuteReader(); //Lance la requête
            return reader;
        }

        //Lance la requête en SqlDataAdapter = mode formulaire
        public SqlDataAdapter AfficherTous(string requeteL)
        {
            connecter(); //Appel méthode de connexion
            commande.Connection = connexion; //Définit au début de la class

            switch (this.choix)
            {
                case "Tous":
                case "FD":
                case "MaxAbsence":
                    commande.CommandText = requeteL; //Paramètre        
                    sda.SelectCommand = commande;    //Lancer la requête            
                    //return sda;                                    
                    break;

                case "Ajouter":
                    try
                    {
                        sda.InsertCommand = new SqlCommand(requeteL, connexion);
                        sda.InsertCommand.ExecuteNonQuery();
                        MessageBox.Show("Enregistrement Absence créé n°" + this.id_absence, "Ajout OK", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;

                case "Supprimer":
                    try
                    {
                        sda.DeleteCommand = new SqlCommand(requeteL, connexion);
                        sda.DeleteCommand.ExecuteNonQuery();
                        MessageBox.Show("Enregistrement Absence supprimé n°" + this.id_absence, "suppression OK", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;

                case "Modifier":
                    try
                    {
                        sda.UpdateCommand = new SqlCommand(requeteL, connexion);
                        sda.UpdateCommand.ExecuteNonQuery();
                        MessageBox.Show("Enregistrement Absence Modifié n°" + this.id_absence, "MàJ OK", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;

                default:
                    Console.WriteLine("Erreur de choix!");
                    break;
            } //Fin switch    
            return sda;
        } //Fin méthode


        //Libérée l'accès à la DB
        public void deconnecter()
        {
            connexion.Close();
        }

    }

    //Fin class Absence



    //***********************************************************
    //Debut class equipement

    class Equipement
    {
        //Connection
        public SqlConnection connexion = new SqlConnection();
        public SqlCommand commande = new SqlCommand();
        private string requete = "SELECT * FROM Employe";
        private string choix; //"Ajouter", "Modifier", "Tous", "FD" & "Supprimer"

        //Attributs Equipement
        private int id_equipement; //int
        private string nom; //nchar(10)
        private string descriptif; //nchar(20)
        private int puissance; //int
        private string etat; //nchar(10)
        private float prix_ht; //float
        private string id_employe; //nchar(15)

        //En premier lieu, il faut instancier un objet SDA d’un type dérivé de la classe DbDataAdapter.
        public SqlDataAdapter sda = new SqlDataAdapter(); //SDA = sda

        //DataSet pour le Formulaire à venir
        public DataSet DsDonnée = new DataSet(); //DataSet1
        public DataTable DtDonnée = new DataTable();  //DataSet2

        //Méthode de connection
        //public object ConnectionState { get; private set; }
        public ConnectionState State
        {
            get { return commande.Connection.State; }
        }

        public void SetId_equipement(int param_id)
        {
            this.id_equipement = param_id;
        }

        public void SetNom(string param_nom)
        {
            this.nom = param_nom;
        }

        public void SetDescriptif(string descriptif)
        {
            this.descriptif = descriptif;
        }

        public void SetPuissance(int type_contrat)
        {
            this.puissance = type_contrat;
        }

        public void SetEtat(string etat)
        {
            this.etat = etat;
        }

        public void SetPrix_ht(float prix_ht)
        {
            this.prix_ht = prix_ht;
        }

        

        public void SetId_employe(string id_employe)
        {
            this.id_employe = id_employe;
        }

       

        //Teste l'état de la connection
        public void connecter()
        {
            //nom db, nom serveur, mode sécurité
            connexion.ConnectionString = "Initial catalog='Dynamic_Jet' ; data source = 'CTI-GEN-008' ; integrated security=true";

            //connexion.ConnectionString = "Initial catalog='Dynamic_Jet' ; data source = 'JBH" + "\\" + "SQLEXPRESS' ; integrated security=true";
            //JBH\SQLEXPRESS
            connexion.Open();
        }

        //Génère une requête SQL
        public string Requete(string choix)
        {
            this.choix = choix;
            switch (choix)
            {
                case "Tous":
                    requete = "SELECT * FROM Equipement";
                    break;
                case "FD":
                    requete = "SELECT * FROM Equipement WHERE id_equipement = " + this.id_equipement;
                    break;
                case "Ajouter":
                    requete = "INSERT INTO Equipement (id_equipement, nom, descriptif, puissance, etat, prix_ht, id_employe) VALUES (" +
                              this.id_equipement + ", '" + this.nom + "', '" + this.descriptif + "', " + this.puissance + ", '" + this.etat + "', " +
                              this.prix_ht + ", " + this.id_employe + ")";
                    //MessageBox.Show("Chaine SQL= " + requete, "Infos Insert", MessageBoxButtons.OK);
                    break;
                case "Supprimer":
                    requete = "DELETE FROM Equipement WHERE id_equipement = " + this.id_equipement;
                    break;
                case "Modifier":
                    requete = "UPDATE Equipement SET id_equipement = " + this.id_equipement + ", nom = '" + this.nom + "', descriptif = '" +
                              this.descriptif + "', puissance = '" + this.puissance +
                              "', etat = '" + this.etat + "', prix_ht = '" + this.prix_ht + "', id_employe = '" + this.id_employe +
                              "' WHERE id_equipement = " + this.id_equipement;
                    break;
                default:
                    Console.WriteLine("Erreur de choix!");
                    break;
            } //Fin de switch          
            return requete;
        }

        //Lance la requête en SqlDataReader = mode console
        public SqlDataReader Afficher1(string requeteL)
        {
            connecter(); //Appel méthode d econnexion
            commande.Connection = connexion; //Définit au début de la class
            commande.CommandText = requeteL; //Paramètre
            SqlDataReader reader = commande.ExecuteReader(); //Lance la requête
            return reader;
        }

        //Lance la requête en SqlDataAdapter = mode formulaire
        public SqlDataAdapter AfficherTous(string requeteL)
        {
            connecter(); //Appel méthode de connexion
            commande.Connection = connexion; //Définit au début de la class

            switch (this.choix)
            {
                case "Tous":
                case "FD":
                    commande.CommandText = requeteL; //Paramètre        
                    sda.SelectCommand = commande;    //Lancer la requête            
                    //return sda;                                    
                    break;

                case "Ajouter":
                    try
                    {
                        sda.InsertCommand = new SqlCommand(requeteL, connexion);
                        sda.InsertCommand.ExecuteNonQuery();
                        MessageBox.Show("Enregistrement Equipement créé n°" + this.id_equipement, "Ajout OK", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;

                case "Supprimer":
                    try
                    {
                        sda.DeleteCommand = new SqlCommand(requeteL, connexion);
                        sda.DeleteCommand.ExecuteNonQuery();
                        MessageBox.Show("Enregistrement Equipement supprimé n°" + this.id_equipement, "suppression OK", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;

                case "Modifier":
                    try
                    {
                        sda.UpdateCommand = new SqlCommand(requeteL, connexion);
                        sda.UpdateCommand.ExecuteNonQuery();
                        MessageBox.Show("Enregistrement Equipement Modifié n°" + this.id_equipement, "MàJ OK", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;

                default:
                    Console.WriteLine("Erreur de choix!");
                    break;
            } //Fin switch    
            return sda;
        } //Fin méthode


        //Libérée l'accès à la DB
        public void deconnecter()
        {
            connexion.Close();
        }

    }

    //Fin class equipement



    //***********************************************************
    //Debut class réservation

    class Reservation
    {
        //Connection
        public SqlConnection connexion = new SqlConnection();
        public SqlCommand commande = new SqlCommand();
        private string requete = "SELECT * FROM Reservation";
        private string choix; //"Ajouter", "Modifier", "Tous", "FD" & "Supprimer"

        //Attributs Reservation      
        private int id_reservation; //int
        private DateTime date_debut; //date
        private DateTime date_fin; //nchar(10)
        private string type; //nchar(50)
        private float cout_total; //nchar(10)
        private int id_client; //nchar(10)
        private int id_equipement; //nchar(10)
        
        //En premier lieu, il faut instancier un objet SDA d’un type dérivé de la classe DbDataAdapter.
        public SqlDataAdapter sda = new SqlDataAdapter(); //SDA = sda

        //DataSet pour le Formulaire à venir
        public DataSet DsDonnée = new DataSet(); //DataSet1
        public DataTable DtDonnée = new DataTable();  //DataSet2

        //Méthode de connection
        //public object ConnectionState { get; private set; }
        public ConnectionState State
        {
            get { return commande.Connection.State; }
        }
        
        public void SetId_reservation(int id_reservation)
        {
            this.id_reservation = id_reservation;
        }

        public void SetDate_debut(DateTime date_debut)
        {
            this.date_debut = date_debut;
        }

        public void SetDate_fin(DateTime date_fin)
        {
            this.date_fin = date_fin;
        }

        public void SetType(string type)
        {
            this.type = type;
        }

        public void SetCout_total(float cout_total)
        {
            this.cout_total = cout_total;
        }

        public void SetId_client(int id_client)
        {
            this.id_client = id_client;
        }

        public void SEtId_equipement(int id_equipement)
        {
            this.id_equipement = id_equipement;
        }
       
        //Teste l'état de la connection
        public void connecter()
        {
            //nom db, nom serveur, mode sécurité
            connexion.ConnectionString = "Initial catalog='Dynamic_Jet' ; data source = 'CTI-GEN-008' ; integrated security=true";

            //connexion.ConnectionString = "Initial catalog='Dynamic_Jet' ; data source = 'JBH" + "\\" + "SQLEXPRESS' ; integrated security=true";
            //JBH\SQLEXPRESS
            connexion.Open();
        }

        //Génère une requête SQL
        public string Requete(string choix)
        {
            this.choix = choix;
            switch (choix)
            {
                case "Tous":
                    requete = "SELECT * FROM Reservation";
                    break;
                case "FD":
                    requete = "SELECT * FROM Reservation WHERE id_reservation = " + this.id_reservation;
                    break;
                case "Ajouter":
                    requete = "INSERT INTO Reservation (id_reservation, date_debut, date_fin, type, cout_total, id_client, id_equipement) VALUES (" +
                              this.id_reservation + ", '" + this.date_debut + "', '" + this.date_fin + "', '" + this.type + "', " + this.cout_total + ", " +
                              this.id_client + ", " + this.id_equipement  + ")";
                    //MessageBox.Show("Chaine SQL= " + requete, "Infos Insert", MessageBoxButtons.OK);
                    break;
                case "Supprimer":
                    requete = "DELETE FROM Reservation WHERE id_reservation = " + this.id_reservation;
                    break;
                case "Modifier":
                    requete = "UPDATE Reservation SET id_reservation = " + this.id_reservation + ", date_debut = '" + this.date_debut + "', date_fin = '" +
                              this.date_fin + "', type = '" + this.type +
                              "', cout_total = " + this.cout_total + ", id_client = " + this.id_client + ", id_equipement = " + this.id_equipement +
                              " WHERE id_reservation = " + this.id_reservation;
                    break;

                case "MonoDispoDDJ":
                    String Date_du_Jour = System.DateTime.Now.ToShortDateString();
                    String Heure_du_Jour = DateTime.Now.ToString("HH:mm");
                    MessageBox.Show("Date du jour =" + Date_du_Jour, "Infos", MessageBoxButtons.OK);
                    MessageBox.Show("Heure du jour =" + Heure_du_Jour, "Infos", MessageBoxButtons.OK);
                    //requete = "SELECT B.* FROM Employe B WHERE B.numero_secu not in (SELECT A.id_employe FROM Absence A WHERE '2001-01-01' between A.date_debut and A.date_fin AND '" + Heure_du_Jour + "' between A.heure_debut and A.heure_fin)";
                    requete = "SELECT B.* FROM Employe B WHERE B.numero_secu not in (SELECT A.id_employe FROM Absence A WHERE '" + Date_du_Jour + "' between A.date_debut and A.date_fin AND '" + Heure_du_Jour + "' between A.heure_debut and A.heure_fin)";
                    break;

                case "MonoDispoPermis":
                    String Date_du_Jour1 = System.DateTime.Now.ToShortDateString();
                    String Heure_du_Jour1 = DateTime.Now.ToString("HH:mm");
                    MessageBox.Show("Date du jour =" + Date_du_Jour1, "Infos", MessageBoxButtons.OK);
                    MessageBox.Show("Heure du jour =" + Heure_du_Jour1, "Infos", MessageBoxButtons.OK);
                    //requete = "SELECT B.* FROM Employe B WHERE B.numero_secu not in (SELECT A.id_employe FROM Absence A WHERE '2001-01-01' between A.date_debut and A.date_fin AND '" + Heure_du_Jour + "' between A.heure_debut and A.heure_fin)";
                    requete = "SELECT B.* FROM Employe B WHERE B.statut_activite = 'Permis' AND B.numero_secu not in (SELECT A.id_employe FROM Absence A WHERE '" + Date_du_Jour1 + "' between A.date_debut and A.date_fin AND '" + Heure_du_Jour1 + "' between A.heure_debut and A.heure_fin)";
                    break;

                case "EquipementDispo":
                    String Date_du_Jour2 = System.DateTime.Now.ToString();                    
                    MessageBox.Show("Date du jour =" + Date_du_Jour2, "Infos", MessageBoxButtons.OK);                    
                    //requete = "SELECT B.* FROM Employe B WHERE B.numero_secu not in (SELECT A.id_employe FROM Absence A WHERE '2001-01-01' between A.date_debut and A.date_fin AND '" + Heure_du_Jour + "' between A.heure_debut and A.heure_fin)";
                    requete = "SELECT B.* FROM Equipement B WHERE B.id_equipement not in (SELECT A.id_equipement FROM Reservation A WHERE '" + Date_du_Jour2 + "' between A.date_debut and A.date_fin)";
                    break;

                case "10Pourcent":
                    MessageBox.Show("Client =" + this.id_client, "Infos", MessageBoxButtons.OK);
                    requete = "SELECT COUNT(*) as 'nbresas', sum (A.cout_total) as 'mtresas' FROM Reservation A WHERE A.id_client = " + this.id_client;
                    break;

                case "Tarif":
                    //MessageBox.Show("Client =" + this.id_client, "Infos", MessageBoxButtons.OK);
                    requete = "SELECT A.* FROM [Tarif] A WHERE A.id_equipement = " + this.id_equipement;
                    break;
                
                default:
                    Console.WriteLine("Erreur de choix!");
                    break;
            } //Fin de switch          
            return requete;
        }

        //Lance la requête en SqlDataReader = mode console
        public SqlDataReader Afficher1(string requeteL)
        {
            connecter(); //Appel méthode d econnexion
            commande.Connection = connexion; //Définit au début de la class
            commande.CommandText = requeteL; //Paramètre
            SqlDataReader reader = commande.ExecuteReader(); //Lance la requête
            return reader;
        }

        //Lance la requête en SqlDataAdapter = mode formulaire
        public SqlDataAdapter AfficherTous(string requeteL)
        {
            connecter(); //Appel méthode de connexion
            commande.Connection = connexion; //Définit au début de la class

            switch (this.choix)
            {
                case "Tous":
                case "FD":
                case "MonoDispoDDJ":
                case "MonoDispoPermis":
                case "EquipementDispo":
                case "10Pourcent":
                case "Tarif":                
                    commande.CommandText = requeteL; //Paramètre        
                    sda.SelectCommand = commande;    //Lancer la requête            
                    //return sda;                                    
                    break;

                case "Ajouter":
                    try
                    {
                        sda.InsertCommand = new SqlCommand(requeteL, connexion);
                        sda.InsertCommand.ExecuteNonQuery();
                        MessageBox.Show("Enregistrement Réservation créé n°" + this.id_reservation, "Ajout OK", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;

                case "Supprimer":
                    try
                    {
                        sda.DeleteCommand = new SqlCommand(requeteL, connexion);
                        sda.DeleteCommand.ExecuteNonQuery();
                        MessageBox.Show("Enregistrement Réservation supprimé n°" + this.id_reservation, "suppression OK", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;

                case "Modifier":
                    try
                    {
                        sda.UpdateCommand = new SqlCommand(requeteL, connexion);
                        sda.UpdateCommand.ExecuteNonQuery();
                        MessageBox.Show("Enregistrement Réservation Modifié n°" + this.id_reservation, "MàJ OK", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;

                default:
                    Console.WriteLine("Erreur de choix!");
                    break;
            } //Fin switch    
            return sda;
        } //Fin méthode


        //Libérée l'accès à la DB
        public void deconnecter()
        {
            connexion.Close();
        }
    }
    //Fin class réservation

} //Fin NS Base_Dynamic_Jet
