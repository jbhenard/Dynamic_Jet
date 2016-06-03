using Base_Dynamic_Jet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication2;

namespace Dynamic_Jet
{
    public partial class DynamicJet : Form
    {
        public DynamicJet()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Tous Employés
        private void button6_Click(object sender, EventArgs e)
        {
            //Début code            
            Employé d = new Employé(); //Class Data avec méthodes car class importer sur même NameSpace
            SqlDataAdapter sda = d.AfficherTous(d.Requete("Tous"));
            DataTable dt = new DataTable();  //DataSet
            sda.Fill(dt);
            dataGridView3.DataSource = dt; //Alimenter le DataGrid avec le DataSet            
            d.deconnecter(); //Fin connexion
            //Fin code
        }

        //Tous Clients
        private void button10_Click(object sender, EventArgs e)
        {
            //Début code            
            Client d = new Client(); //Class Data avec méthodes car class importer sur même NameSpace
            SqlDataAdapter sda = d.AfficherTous(d.Requete("Tous"));
            DataTable dt = new DataTable();  //DataSet
            sda.Fill(dt);
            dataGridView1.DataSource = dt; //Alimenter le DataGrid avec le DataSet                     
            d.deconnecter(); //Fin connexion
            //Fin code
        }
        
        //FD Client
        private void button9_Click(object sender, EventArgs e)
        {
            //Début code                       
            Client d = new Client(); //Class Data avec méthodes car class importer sur même NameSpace

            //Teste si null
            if (String.IsNullOrEmpty(textBox1.Text)){
                MessageBox.Show("Saisir un identifant Client à consulter SVP !!! ", "Erreur de saisie", MessageBoxButtons.OK);
            }else{                
                d.SetId_client(Int32.Parse(textBox1.Text));
                SqlDataAdapter sda = d.AfficherTous(d.Requete("FD"));
                DataTable dt = new DataTable();  //DataSet
                sda.Fill(dt);
                dataGridView1.DataSource = dt; //Alimenter le DataGrid avec le DataSet            
                d.deconnecter(); //Fin connexion            
            } //Fin else
            //Fin code
        }

        //Ajout Client(s)
        private void button8_Click(object sender, EventArgs e)
        {
            //Début code            
            
                //Début saisie Ajout Client(s)
                FormClient2 Formu_Ajout = new FormClient2();
                Formu_Ajout.Text = "Ajout Client(s)";
                Formu_Ajout.Show();
                //Fin saisie Ajout Client(s)

                Client d = new Client(); //Class Data avec méthodes car class importer sur même NameSpace
                SqlDataAdapter sda = d.AfficherTous(d.Requete("Tous"));
                DataTable dt = new DataTable();  //DataSet = mode déconnecté donc pasconnexion base en continue
                sda.Fill(dt);
                dataGridView1.DataSource = dt; //Alimenter le DataGrid avec le DataSet            

                d.deconnecter(); //Fin connexion
            
            //Fin code
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //Début code            
            Reservation d = new Reservation(); //Class Data avec méthodes car class importer sur même NameSpace
            SqlDataAdapter sda = d.AfficherTous(d.Requete("Tous"));
            DataTable dt = new DataTable();  //DataSet
            sda.Fill(dt);
            dataGridView2.DataSource = dt; //Alimenter le DataGrid avec le DataSet            

            d.deconnecter(); //Fin connexion
            //Fin code
        }

        //Supprimer Client(s)
        private void button7_Click(object sender, EventArgs e)
        {
            //Début code            
            Client d = new Client(); //Class Data avec méthodes car class importer sur même NameSpace
            //Teste si null
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Saisir un identifant Client à supprimer !!! ", "Erreur de saisie", MessageBoxButtons.OK);
            }
            else
            {
                d.SetId_client(Int32.Parse(textBox1.Text));
                SqlDataAdapter sda = d.AfficherTous(d.Requete("Supprimer"));                
                d.deconnecter(); //Fin connexion
               
                Client d2 = new Client(); //Class Data avec méthodes car class importer sur même NameSpace
                SqlDataAdapter sda2 = d2.AfficherTous(d2.Requete("Tous"));
                DataTable dt2 = new DataTable();  //DataSet = mode déconnecté donc pasconnexion base en continue
                sda2.Fill(dt2);
                dataGridView1.DataSource = dt2; //Alimenter le DataGrid avec le DataSet            

            }
            //Fin code
        }


        //Modifier Client(s)
        private void button3_Click(object sender, EventArgs e)
        {
            //Début code            
            //Teste si null
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Saisir un identifant Client à modifier !!! ", "Erreur de saisie", MessageBoxButtons.OK);
            }
            else
            {
                //Début saisie Modifier Client(s)
                FormClient2 Formu_Modifier = new FormClient2();
                Formu_Modifier.Text = "Modifier Client(s)";
                Formu_Modifier.SetTextBox1(textBox1.Text); //Passage de l'identifiant de Form1.textBox1.Text vars Form2.textBox1.Text via un setter.
                Formu_Modifier.Show();
            }//Fin saisie Modifier Client(s)

            Client d = new Client(); //Class Data avec méthodes car class importer sur même NameSpace
            SqlDataAdapter sda = d.AfficherTous(d.Requete("Tous"));
            DataTable dt = new DataTable();  //DataSet = mode déconnecté donc pasconnexion base en continue
            sda.Fill(dt);
            dataGridView1.DataSource = dt; //Alimenter le DataGrid avec le DataSet            
            d.deconnecter(); //Fin connexion
            //Fin code
        }


        //FD Employé
        private void button5_Click(object sender, EventArgs e)
        {
            //Début code                       
            Employé d = new Employé(); //Class Data avec méthodes car class importer sur même NameSpace

            //Teste si null
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Saisir un identifant Employé à consulter SVP !!! ", "Erreur de saisie", MessageBoxButtons.OK);
            }
            else
            {                
                d.SetNumero_secu(textBox2.Text);
                SqlDataAdapter sda = d.AfficherTous(d.Requete("FD"));
                DataTable dt = new DataTable();  //DataSet
                sda.Fill(dt);
                dataGridView3.DataSource = dt; //Alimenter le DataGrid avec le DataSet            
                d.deconnecter(); //Fin connexion            
            } //Fin else
            //Fin code
        }


        //Ajouter Employé(s)
        private void button4_Click(object sender, EventArgs e)
        {
            //Début code            

            //Début saisie Ajout Employé(s)
            FormEmployé Formu_Ajout = new FormEmployé();
            Formu_Ajout.Text = "Ajout Employé(s)";
            Formu_Ajout.Show();
            //Fin saisie Ajout Employé(s)

            Employé d = new Employé(); //Class Data avec méthodes car class importer sur même NameSpace
            SqlDataAdapter sda = d.AfficherTous(d.Requete("Tous"));
            DataTable dt = new DataTable();  //DataSet = mode déconnecté donc pasconnexion base en continue
            sda.Fill(dt);
            dataGridView3.DataSource = dt; //Alimenter le DataGrid avec le DataSet            
            d.deconnecter(); //Fin connexion
            //Fin code
        }

        //Supprimer Employé(s)
        private void button1_Click(object sender, EventArgs e)
        {
            //Début code            
            Employé d = new Employé(); //Class Data avec méthodes car class importer sur même NameSpace
            //Teste si null
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Saisir un identifant Employé à supprimer !!! ", "Erreur de saisie", MessageBoxButtons.OK);
            }
            else
            {
                //d.SetNumero_secu(Int32.Parse(textBox2.Text));
                d.SetNumero_secu(textBox2.Text);
                SqlDataAdapter sda = d.AfficherTous(d.Requete("Supprimer"));                
                d.deconnecter(); //Fin connexion
                
                Employé d2 = new Employé(); //Class Data avec méthodes car class importer sur même NameSpace
                SqlDataAdapter sda2 = d2.AfficherTous(d2.Requete("Tous"));
                DataTable dt2 = new DataTable();  //DataSet = mode déconnecté donc pasconnexion base en continue
                sda2.Fill(dt2);
                dataGridView3.DataSource = dt2; //Alimenter le DataGrid avec le DataSet            
            }
            //Fin code
        }


        //Modifier Employé
        private void button2_Click(object sender, EventArgs e)
        {

            //Début code            
            //Teste si null
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Saisir un identifant Employé à modifier !!! ", "Erreur de saisie", MessageBoxButtons.OK);
            }
            else
            {
                //Début saisie Modifier Employé(s)
                FormEmployé Formu_Modifier = new FormEmployé();
                Formu_Modifier.Text = "Modifier Employé(s)";
                Formu_Modifier.SetTextBox1(textBox2.Text); //Passage de l'identifiant de Form1.textBox1.Text vars Form2.textBox1.Text via un setter.
                Formu_Modifier.Show();
            }//Fin saisie Modifier Employé(s)

            Employé d = new Employé(); //Class Data avec méthodes car class importer sur même NameSpace
            SqlDataAdapter sda = d.AfficherTous(d.Requete("Tous"));
            DataTable dt = new DataTable();  //DataSet = mode déconnecté donc pasconnexion base en continue
            sda.Fill(dt);
            dataGridView3.DataSource = dt; //Alimenter le DataGrid avec le DataSet            

            d.deconnecter(); //Fin connexion
            //Fin code
        }

        //Tous Equipement
        private void button24_Click(object sender, EventArgs e)
        {
            //Début code            
            Equipement d = new Equipement(); //Class Data avec méthodes car class importer sur même NameSpace
            SqlDataAdapter sda = d.AfficherTous(d.Requete("Tous"));
            DataTable dt = new DataTable();  //DataSet
            sda.Fill(dt);
            dataGridView4.DataSource = dt; //Alimenter le DataGrid avec le DataSet            
            d.deconnecter(); //Fin connexion
            //Fin code
        }

        //FD Equipement
        private void button23_Click(object sender, EventArgs e)
        {
            //Début code                       
            Equipement d = new Equipement(); //Class Data avec méthodes car class importer sur même NameSpace

            //Teste si null
            if (String.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Saisir un Equipement à consulter SVP !!! ", "Erreur de saisie", MessageBoxButtons.OK);
            }
            else
            {
                d.SetId_equipement(Int32.Parse(textBox3.Text));
                SqlDataAdapter sda = d.AfficherTous(d.Requete("FD"));
                DataTable dt = new DataTable();  //DataSet
                sda.Fill(dt);
                dataGridView4.DataSource = dt; //Alimenter le DataGrid avec le DataSet            

                d.deconnecter(); //Fin connexion            
            } //Fin else
            //Fin code
        }

        //Supprimer Equipement
        private void button21_Click(object sender, EventArgs e)
        {
            //Début code            
            Equipement d = new Equipement(); //Class Data avec méthodes car class importer sur même NameSpace
            //Teste si null
            if (String.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Saisir un Equipement à supprimer !!! ", "Erreur de saisie", MessageBoxButtons.OK);
            }
            else
            {
                d.SetId_equipement(Int32.Parse(textBox3.Text));
                SqlDataAdapter sda = d.AfficherTous(d.Requete("Supprimer"));                
                d.deconnecter(); //Fin connexion
                
                Client d2 = new Client(); //Class Data avec méthodes car class importer sur même NameSpace
                SqlDataAdapter sda2 = d2.AfficherTous(d2.Requete("Tous"));
                DataTable dt2 = new DataTable();  //DataSet = mode déconnecté donc pasconnexion base en continue
                sda2.Fill(dt2);
                dataGridView4.DataSource = dt2; //Alimenter le DataGrid avec le DataSet            
            }
            //Fin code
        }

        //Ajouter Equipement
        private void button22_Click(object sender, EventArgs e)
        {
            //Début code            

            //Début saisie Ajout Equipement(s)
            FormEquipement Formu_Ajout = new FormEquipement();
            Formu_Ajout.Text = "Ajout Equipement(s)";
            Formu_Ajout.Show();
            //Fin saisie Ajout Equipement(s)

            Equipement d = new Equipement(); //Class Data avec méthodes car class importer sur même NameSpace
            SqlDataAdapter sda = d.AfficherTous(d.Requete("Tous"));
            DataTable dt = new DataTable();  //DataSet = mode déconnecté donc pasconnexion base en continue
            sda.Fill(dt);
            dataGridView4.DataSource = dt; //Alimenter le DataGrid avec le DataSet            
            d.deconnecter(); //Fin connexion
            //Fin code
        }

        //Modifier Equipement
        private void button20_Click(object sender, EventArgs e)
        {
            //Début code            
            //Teste si null
            if (String.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Saisir un Equipement à modifier !!! ", "Erreur de saisie", MessageBoxButtons.OK);
            }
            else
            {
                //Début saisie Modifier Equipement(s)
                FormEquipement Formu_Modifier = new FormEquipement();
                Formu_Modifier.Text = "Modifier Equipement(s)";
                Formu_Modifier.SetTextBox1(textBox3.Text); //Passage de l'identifiant de Form1.textBox1.Text vars Form2.textBox1.Text via un setter.
                Formu_Modifier.Show();
            }//Fin saisie Modifier Equipement(s)

            Equipement d = new Equipement(); //Class Data avec méthodes car class importer sur même NameSpace
            SqlDataAdapter sda = d.AfficherTous(d.Requete("Tous"));
            DataTable dt = new DataTable();  //DataSet = mode déconnecté donc pasconnexion base en continue
            sda.Fill(dt);
            dataGridView4.DataSource = dt; //Alimenter le DataGrid avec le DataSet            
            d.deconnecter(); //Fin connexion
            //Fin code
        }

        //Tous Absences
        private void button29_Click(object sender, EventArgs e)
        {
            //Début code            
            Absence d = new Absence(); //Class Data avec méthodes car class importer sur même NameSpace
            SqlDataAdapter sda = d.AfficherTous(d.Requete("Tous"));
            DataTable dt = new DataTable();  //DataSet
            sda.Fill(dt);
            dataGridView3.DataSource = dt; //Alimenter le DataGrid avec le DataSet            
            d.deconnecter(); //Fin connexion
            //Fin code
        }

        //FD Absence
        private void button28_Click(object sender, EventArgs e)
        {
            //Début code                       

            Absence d = new Absence(); //Class Data avec méthodes car class importer sur même NameSpace

            //Teste si null
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Saisir un identifant Employé à consulter SVP !!! ", "Erreur de saisie", MessageBoxButtons.OK);
            }
            else
            {                
                d.SetId_absence(textBox2.Text);
                SqlDataAdapter sda = d.AfficherTous(d.Requete("FD"));
                DataTable dt = new DataTable();  //DataSet
                sda.Fill(dt);
                dataGridView3.DataSource = dt; //Alimenter le DataGrid avec le DataSet            
                d.deconnecter(); //Fin connexion            
            } //Fin else
            //Fin code
        }

        //Ajouter Absences
        private void button27_Click(object sender, EventArgs e)
        {
            //Début code            

            //Début saisie Ajout Absence(s)
            FormAbsence Formu_Ajout = new FormAbsence();
            Formu_Ajout.Text = "Ajout Absence(s)";
            Formu_Ajout.Show();
            //Fin saisie Ajout Absence(s)

            Absence d = new Absence(); //Class Data avec méthodes car class importer sur même NameSpace
            SqlDataAdapter sda = d.AfficherTous(d.Requete("Tous"));
            DataTable dt = new DataTable();  //DataSet = mode déconnecté donc pasconnexion base en continue
            sda.Fill(dt);
            dataGridView3.DataSource = dt; //Alimenter le DataGrid avec le DataSet            

            d.deconnecter(); //Fin connexion
            //Fin code
        }

        //Supprimer Absence(s)
        private void button26_Click(object sender, EventArgs e)
        {
            //Début code            
            Absence d = new Absence(); //Class Data avec méthodes car class importer sur même NameSpace
            //Teste si null
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Saisir une Absence à supprimer !!! ", "Erreur de saisie", MessageBoxButtons.OK);
            }
            else
            {                
                d.SetId_absence(textBox2.Text);
                SqlDataAdapter sda = d.AfficherTous(d.Requete("Supprimer"));                
                d.deconnecter(); //Fin connexion
             
                Employé d2 = new Employé(); //Class Data avec méthodes car class importer sur même NameSpace
                SqlDataAdapter sda2 = d2.AfficherTous(d2.Requete("Tous"));
                DataTable dt2 = new DataTable();  //DataSet = mode déconnecté donc pasconnexion base en continue
                sda2.Fill(dt2);
                dataGridView3.DataSource = dt2; //Alimenter le DataGrid avec le DataSet            
            }
            //Fin code
        }

        //Modifier Absence(s)
        private void button25_Click(object sender, EventArgs e)
        {

            //Début code            
            //Teste si null
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Saisir une Absence à modifier !!! ", "Erreur de saisie", MessageBoxButtons.OK);
            }
            else
            {
                //Début saisie Modifier Employé(s)
                FormAbsence Formu_Modifier = new FormAbsence();
                Formu_Modifier.Text = "Modifier Absence(s)";
                Formu_Modifier.SetTextBox4(textBox2.Text); //Passage de l'identifiant de Form1.textBox1.Text vars Form2.textBox1.Text via un setter.
                Formu_Modifier.Show();
            }//Fin saisie Modifier Employé(s)

            Absence d = new Absence(); //Class Data avec méthodes car class importer sur même NameSpace
            SqlDataAdapter sda = d.AfficherTous(d.Requete("Tous"));
            DataTable dt = new DataTable();  //DataSet = mode déconnecté donc pasconnexion base en continue
            sda.Fill(dt);
            dataGridView3.DataSource = dt; //Alimenter le DataGrid avec le DataSet            
            d.deconnecter(); //Fin connexion
            //Fin code
        }

        //FD Réservation
        private void button14_Click(object sender, EventArgs e)
        {
            //Début code                       

            Reservation d = new Reservation(); //Class Data avec méthodes car class importer sur même NameSpace

            //Teste si null
            if (String.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Saisir une Réservation à consulter SVP !!! ", "Erreur de saisie", MessageBoxButtons.OK);
            }
            else
            {
                d.SetId_reservation(Int32.Parse(textBox4.Text));
                SqlDataAdapter sda = d.AfficherTous(d.Requete("FD"));
                DataTable dt = new DataTable();  //DataSet
                sda.Fill(dt);
                dataGridView2.DataSource = dt; //Alimenter le DataGrid avec le DataSet            
                d.deconnecter(); //Fin connexion            
            } //Fin else
            //Fin code
        }

        //Supprimer Réservation
        private void button12_Click(object sender, EventArgs e)
        {
            //Début code            
            Reservation d = new Reservation(); //Class Data avec méthodes car class importer sur même NameSpace
            //Teste si null
            if (String.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Saisir une Réservation à supprimer !!! ", "Erreur de saisie", MessageBoxButtons.OK);
            }
            else
            {
                d.SetId_reservation(Int32.Parse(textBox4.Text));
                SqlDataAdapter sda = d.AfficherTous(d.Requete("Supprimer"));                
                d.deconnecter(); //Fin connexion
                
                Reservation d2 = new Reservation(); //Class Data avec méthodes car class importer sur même NameSpace
                SqlDataAdapter sda2 = d2.AfficherTous(d2.Requete("Tous"));
                DataTable dt2 = new DataTable();  //DataSet = mode déconnecté donc pasconnexion base en continue
                sda2.Fill(dt2);
                dataGridView2.DataSource = dt2; //Alimenter le DataGrid avec le DataSet            
            }
            //Fin code
        }

        //Ajouter Réservation
        private void button13_Click(object sender, EventArgs e)
        {
            //Début code            
            //Teste si null
            if (!String.IsNullOrEmpty(textBox4.Text))
            {
                //Traitement 10% réduction: si Mt des 10 dernières réservation >= 1300€
                Reservation d10 = new Reservation(); //Class Data avec méthodes car class importer sur même NameSpace
                d10.SetId_client(Int32.Parse(textBox4.Text));
                SqlDataAdapter sda10 = d10.AfficherTous(d10.Requete("10Pourcent"));

                DataTable table10 = new DataTable();  //DataSet = mode déconnecté donc pasconnexion base en continue
                sda10.Fill(table10);
                
                //if (dt10.Rows.Count > 0) MessageBox.Show("Bon Client (10 der résa & Mt total >= 1300€), bénéficie de 10% de réduction !!! ", "Infos Réservation", MessageBoxButtons.OK);
                if (!String.IsNullOrEmpty(table10.Rows[0][0].ToString()) && !String.IsNullOrEmpty(table10.Rows[0][1].ToString()))
                {
                    if (Int32.Parse(table10.Rows[0][0].ToString()) == 10 || Int32.Parse(table10.Rows[0][1].ToString()) >= 1300) MessageBox.Show("Bon Client (10 der résa & Mt total >= 1300€), bénéficie de 10% de réduction !!! ", "Infos Réservation", MessageBoxButtons.OK);
                    else MessageBox.Show("Pas de remise !!! ", "Infos Réservation", MessageBoxButtons.OK);
                }
                else MessageBox.Show("Pas de remise !!! ", "Infos Réservation", MessageBoxButtons.OK);
            } //Fin Si


            //Début saisie Ajout Réservation(s)
            Form1 Formu_Ajout = new Form1();
                Formu_Ajout.Text = "Ajout Réservation(s)";
                Formu_Ajout.Show();
                //Fin saisie Ajout Réservation(s)

                Reservation d = new Reservation(); //Class Data avec méthodes car class importer sur même NameSpace
                SqlDataAdapter sda = d.AfficherTous(d.Requete("Tous"));
                DataTable dt = new DataTable();  //DataSet = mode déconnecté donc pasconnexion base en continue
                sda.Fill(dt);
                dataGridView2.DataSource = dt; //Alimenter le DataGrid avec le DataSet            
                d.deconnecter(); //Fin connexion
         //Fin code            
        }

        //Modifier Réservation
        private void button11_Click(object sender, EventArgs e)
        {

            //Début code            
            //Teste si null
            if (String.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Saisir un identifant Client à modifier !!! ", "Erreur de saisie", MessageBoxButtons.OK);
            }
            else
            {
                //Début saisie Modifier Client(s)
                Form1 Formu_Modifier = new Form1();
                Formu_Modifier.Text = "Modifier Réservation(s)";
                Formu_Modifier.SetTextBox1(textBox4.Text); //Passage de l'identifiant de Form1.textBox1.Text vars Form2.textBox1.Text via un setter.
                Formu_Modifier.Show();
            }//Fin saisie Modifier Client(s)

            Reservation d = new Reservation(); //Class Data avec méthodes car class importer sur même NameSpace
            SqlDataAdapter sda = d.AfficherTous(d.Requete("Tous"));
            DataTable dt = new DataTable();  //DataSet = mode déconnecté donc pasconnexion base en continue
            sda.Fill(dt);
            dataGridView1.DataSource = dt; //Alimenter le DataGrid avec le DataSet            
            d.deconnecter(); //Fin connexion
            //Fin code
        }

        //Liste des mono dispo à la date de réservation
        private void button17_Click(object sender, EventArgs e)
        {
            //Début code            
            Reservation d = new Reservation(); //Class Data avec méthodes car class importer sur même NameSpace
            SqlDataAdapter sda = d.AfficherTous(d.Requete("MonoDispoDDJ"));
            DataTable dt = new DataTable();  //DataSet
            sda.Fill(dt);
            dataGridView2.DataSource = dt; //Alimenter le DataGrid avec le DataSet            
            d.deconnecter(); //Fin connexion
            //Fin code
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //Début code            
            Reservation d = new Reservation(); //Class Data avec méthodes car class importer sur même NameSpace
            SqlDataAdapter sda = d.AfficherTous(d.Requete("MonoDispoPermis"));
            DataTable dt = new DataTable();  //DataSet
            sda.Fill(dt);
            dataGridView2.DataSource = dt; //Alimenter le DataGrid avec le DataSet            
            d.deconnecter(); //Fin connexion
            //Fin code
        }


        //Equipement dispo
        private void button16_Click(object sender, EventArgs e)
        {
            //Début code            
            Reservation d = new Reservation(); //Class Data avec méthodes car class importer sur même NameSpace
            SqlDataAdapter sda = d.AfficherTous(d.Requete("EquipementDispo"));
            DataTable dt = new DataTable();  //DataSet
            sda.Fill(dt);
            dataGridView2.DataSource = dt; //Alimenter le DataGrid avec le DataSet            
            d.deconnecter(); //Fin connexion
            //Fin code
        }

        private void resa_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            PaintEventArgs myPaintArgs = new PaintEventArgs(e.Graphics, new Rectangle(new Point(0, 0), this.Size));
            this.InvokePaint(dataGridView2, myPaintArgs);
        }


        //Impression réservation 1
        private void button19_Click(object sender, EventArgs e)
        {
            //Début code                       

            Reservation d = new Reservation(); //Class Data avec méthodes car class importer sur même NameSpace

            //Teste si null
            if (String.IsNullOrEmpty(textBox4.Text)) //Numéro résa
            {
                MessageBox.Show("Saisir une Réservation à consulter SVP !!! ", "Erreur de saisie", MessageBoxButtons.OK);
            }
            else
            {
                d.SetId_reservation(Int32.Parse(textBox4.Text));
                SqlDataAdapter sda = d.AfficherTous(d.Requete("FD"));
                DataTable dt = new DataTable();  //DataSet
                sda.Fill(dt);
                dataGridView2.DataSource = dt; //Alimenter le DataGrid avec le DataSet                                            
                d.deconnecter(); //Fin connexion            

                //Début print
                PrintDocument resa = new PrintDocument(); //Instance de la class PrintDocument
                resa.Print();
                //Fin print
            } //Fin else
            //Fin code
        }

        //Gestion Anti-doublon
        private void button30_Click(object sender, EventArgs e)
        {
            //Début code            
            //Début saisie Doublon Client(s)
            FormClient2 Formu_Ajout = new FormClient2();
            Formu_Ajout.Text = "Vérification doublon Client(s)";
            Formu_Ajout.Show();
            //Fin saisie Doublon Client(s)
            //Fin code
        }

        //Les réservation du client
        private void button31_Click(object sender, EventArgs e)
        {
            //Début code                       

            Client d = new Client(); //Class Data avec méthodes car class importer sur même NameSpace

            //Teste si null
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Saisir un Client à consulter SVP !!! ", "Erreur de saisie", MessageBoxButtons.OK);
            }
            else
            {
                d.SetId_client(Int32.Parse(textBox1.Text));
                SqlDataAdapter sda = d.AfficherTous(d.Requete("RésaClt"));
                DataTable dt = new DataTable();  //DataSet
                sda.Fill(dt);
                dataGridView1.DataSource = dt; //Alimenter le DataGrid avec le DataSet            
                d.deconnecter(); //Fin connexion            
            } //Fin else
            //Fin code            
        }

        //Les absences d'un employé
        private void button32_Click(object sender, EventArgs e)
        {
            //Début code                       

            Employé d = new Employé(); //Class Data avec méthodes car class importer sur même NameSpace

            //Teste si null
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Saisir Employé à consulter SVP !!! ", "Erreur de saisie", MessageBoxButtons.OK);
            }
            else
            {
                //d.SetNumero_secu(Int32.Parse(textBox2.Text));
                d.SetNumero_secu(textBox2.Text);
                SqlDataAdapter sda = d.AfficherTous(d.Requete("AbsencesEmployé"));
                DataTable dt = new DataTable();  //DataSet
                sda.Fill(dt);
                dataGridView3.DataSource = dt; //Alimenter le DataGrid avec le DataSet            
                d.deconnecter(); //Fin connexion            
            } //Fin else
            //Fin code
        }
    }
}
