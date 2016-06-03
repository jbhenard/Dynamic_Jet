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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        public void SetTextBox1(string param1)
        {
            this.textBox1.Text = param1;
        }

        private void Form6_Load(object sender, EventArgs e)
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

        //Modifier Réservation
        private void button1_Click(object sender, EventArgs e)
        {
            //Début code commun Ajout & Modifier            
            Reservation d = new Reservation(); //Class Data avec méthodes car class importer sur même NameSpace
            d.SetId_reservation(Int32.Parse(textBox1.Text)); //int id reservztion
            d.SetDate_debut(DateTime.Parse(textBox2.Text)); // date debut
            d.SetDate_fin(DateTime.Parse(textBox3.Text)); // date fin
            d.SetType(textBox4.Text); //string type
            d.Setcout_total(float.Parse(textBox5.Text)); //float cout total
            d.SetId_client(Int32.Parse(textBox6.Text)); //int id client
            d.SetId_equipement(Int32.Parse(textBox7.Text)); //int id equipement

            if (this.button1.Text == "&Ajout")
            {
                SqlDataAdapter sda = d.AfficherTous(d.Requete("Ajouter"));
            }
            else
            {
                SqlDataAdapter sda = d.AfficherTous(d.Requete("Modifier"));
            }
            d.deconnecter(); //Fin connexion
            //MessageBox.Show("Enregistrement crée !!! ", "Saisie OK", MessageBoxButtons.OK);

            //Form2.ActiveForm.Close();                

            //Fin code
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
