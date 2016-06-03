using Base_Dynamic_Jet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Setter pour communiquer du formulaire Réservation à ce formulaire
        public void SetTextBox1(string param1)
        {
            this.textBox1.Text = param1;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Début code
            if (this.Text == "Ajout Réservation(s)") this.button1.Text = "&Ajout";
            else
            {
                this.button1.Text = "&Modifier";

                if (String.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Saisir une Réservation à modifier !!! ", "Erreur de saisie", MessageBoxButtons.OK);
                }
                else
                {
                    Reservation d = new Reservation(); //Class Data avec méthodes car class importer sur même NameSpace
                    d.SetId_reservation(Int32.Parse(textBox1.Text));
                    SqlDataAdapter sda = d.AfficherTous(d.Requete("FD"));
                    DataTable table = new DataTable();  //DataSet
                    sda.Fill(table);
                    //textBox1.Text = table.Rows[0][0].ToString(); //Alimenter le champs id avec la valeur passé en paramètre
                    textBox2.Text = table.Rows[0][1].ToString(); //Alimenter le champs Nom avec resultat select sur id passé en paramètre
                    textBox3.Text = table.Rows[0][2].ToString(); //Alimenter le champs Prénom avec resultat select sur id passé en paramètre
                    textBox4.Text = table.Rows[0][3].ToString(); //Alimenter le champs Adresse avec resultat select sur id passé en paramètre
                    textBox5.Text = table.Rows[0][4].ToString(); //Alimenter le champs Téléphone avec resultat select sur id passé en paramètre
                    textBox6.Text = table.Rows[0][5].ToString(); //Alimenter le champs Date de naissance avec resultat select sur id passé en paramètre
                    textBox7.Text = table.Rows[0][6].ToString(); //Alimenter le champs N° de permis avec resultat select sur id passé en paramètre
                    
                    d.deconnecter(); //Fin connexion   
                }//Fin Si2
            } //Fin Si1
            //Fin code
        }

        //Ajout Réservation & une absence spécifique réservation
        private void button1_Click(object sender, EventArgs e)
        {
            //Début code commun Ajout & Modifier réservation       
            Reservation d = new Reservation(); //Class Data avec méthodes car class importer sur même NameSpace

            //Supprimer les blancs inutiles avant ajout Réservation
            d.SetId_reservation(Int32.Parse(textBox1.Text)); //int idreservation
            d.SetDate_debut(DateTime.Parse(textBox2.Text)); // date début
            d.SetDate_fin(DateTime.Parse(textBox3.Text)); // date de fin            
            d.SetType(textBox4.Text.Trim()); // type = avec mono sans mono,O/N?
            d.SetCout_total(float.Parse(textBox5.Text)); //cout toral
            d.SetId_client(Int32.Parse(textBox6.Text)); //client
            d.SEtId_equipement(Int32.Parse(textBox7.Text)); // equipement

            if (this.button1.Text == "&Ajout")
            {
                SqlDataAdapter sda = d.AfficherTous(d.Requete("Ajouter"));
            }
            else
            {
                SqlDataAdapter sda = d.AfficherTous(d.Requete("Modifier"));
            }
            d.deconnecter(); //Fin connexion       Résrvation

            if (this.button1.Text == "&Ajout")
            {
                //*******************************************************Instance 1 de la class Absence
                //Ajout Absence liée à l'AJOUT SEULEMENT d'une réservation
                //Début code            
                Absence dMAbsence = new Absence(); //Class pour recherche compteur
                                                   //Recherche max id de absence & faire + 1
                SqlDataAdapter sdaMaxAbsence = dMAbsence.AfficherTous(dMAbsence.Requete("MaxAbsence"));
                DataTable table = new DataTable();  //DataSet
                sdaMaxAbsence.Fill(table);
                int compteur = 9999; //Si erreur
                if (!String.IsNullOrEmpty(table.Rows[0][0].ToString()))
                {
                    compteur = Int32.Parse(table.Rows[0][0].ToString()) + 1;

                }
                else MessageBox.Show("Pas de compteur en table Absence !!! ", "Infos Réservation", MessageBoxButtons.OK);
                dMAbsence.deconnecter(); //Fin connexion       

                //*********************************************Intance 2 de la class Absence
                Absence dAbsence = new Absence(); //Class pour Ajout absence
                dAbsence.SetId_employe(textBox10.Text); //nnchar(15) id employe
                dAbsence.SetDate_debut(DateTime.Parse(textBox2.Text)); //datetime
                dAbsence.SetDate_fin(DateTime.Parse(textBox3.Text)); //datetime                                   
                dAbsence.SetId_absence(compteur.ToString()); //nchar(50) id absence
                dAbsence.SetMotif("RESA"); //nchar(10)    motif

                SqlDataAdapter sdaAbsence = dAbsence.AfficherTous(dAbsence.Requete("Ajouter"));
                dAbsence.deconnecter(); //Fin connexion       
                //Fin code absence
            } //Fin si
            //Fin code
        }

        //Calcul de la facture
        private void button2_Click(object sender, EventArgs e)
        {
            //Déut code
              
            //         
            // Difference in days, hours, and minutes.
            TimeSpan ts = DateTime.Parse(textBox3.Text) - DateTime.Parse(textBox2.Text);

            // Difference in days.
            int differenceInDays = ts.Days;

            //Différence en hours
            int differenceInHours = ts.Hours;
            int differenceInHoursMinutes = differenceInHours * 60; //convertion heure en minutes
            int differenceInMinutes = ts.Minutes;
            int totalMinutes = differenceInMinutes + differenceInHoursMinutes ;

            MessageBox.Show("durée réservation= " + differenceInHours + ":" + differenceInMinutes, "Infos saisie", MessageBoxButtons.OK);

            //+20 % avec mono
            float AvecMono = 1.0F;
            float prixR = 0.0F;
            if (textBox4.Text.ToUpper() == "O")
            {
                AvecMono=1.2F;
            }

            //Recherche du tarif en table
            //Teste si null de id equipement
            if (!String.IsNullOrEmpty(textBox7.Text))
            {
                //Traitement tarif
                Reservation dtarif = new Reservation(); //Class Data avec méthodes car class importer sur même NameSpace
                dtarif.SEtId_equipement(Int32.Parse(textBox7.Text)); //id equipement
                SqlDataAdapter sdatarif = dtarif.AfficherTous(dtarif.Requete("Tarif"));

                DataTable tabletarif = new DataTable();  //DataSet = mode déconnecté donc pasconnexion base en continue
                sdatarif.Fill(tabletarif);
                
                if (!String.IsNullOrEmpty(tabletarif.Rows[0][0].ToString()))
                {
                    float prix = 0;

                    //prix_30mn : Int32.Parse(textBox8.Text) = Nb personnes
                    if (totalMinutes == 30)
                    {   //PEC 20% ou pas
                        prixR = AvecMono * float.Parse(tabletarif.Rows[0][0].ToString());
                        prix = prixR * Int32.Parse(textBox8.Text);                        
                    }
                    
                    //prix_1h
                    if (totalMinutes == 60)
                    {
                        prixR = AvecMono * float.Parse(tabletarif.Rows[0][1].ToString());
                        prix = prixR * Int32.Parse(textBox8.Text);
                    }

                    //prix_1h30
                    if (totalMinutes == 90)
                    {
                        prixR = AvecMono * float.Parse(tabletarif.Rows[0][0].ToString());
                        float prixR2 = AvecMono * float.Parse(tabletarif.Rows[0][1].ToString());
                        prix = (prixR * Int32.Parse(textBox8.Text)) + (prixR2 * Int32.Parse(textBox8.Text));
                    }

                    //prix_2h
                    if (totalMinutes == 120)
                    {
                        prixR = AvecMono * float.Parse(tabletarif.Rows[0][2].ToString());
                        prix = prixR * Int32.Parse(textBox8.Text);                       
                    }

                    //prix > 2h
                    if (totalMinutes > 120)
                    {
                        prixR = AvecMono * float.Parse(tabletarif.Rows[0][2].ToString());
                        prix =((totalMinutes * prixR)/120 ) * Int32.Parse(textBox8.Text);
                    }

                    //remise -10% O/N ?
                    float Avec10 = 1.0F;                    
                    if (textBox9.Text.ToUpper() == "O")
                    {
                        Avec10 = 0.9F;
                        prix = prix * Avec10;
                    }
                    textBox5.Text = prix.ToString(); //zone coû totale

                }//Fin si                
            } //Fin Si
            //Fin code
        }
    }
}
