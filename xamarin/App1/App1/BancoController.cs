using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Database.Sqlite;

namespace App1
{
    public class BancoController
    {
        private SQLiteDatabase db;
        private CriaBanco banco;

        public BancoController(Context context)
        {
            banco = new CriaBanco(context);
        }

        public bool insereDado(String lat, String lon)
        {
            ContentValues valores;
            long resultado;

            db = banco.WritableDatabase;
            valores = new ContentValues();
            valores.Put(CriaBanco.getLAT(), lat);
            valores.Put(CriaBanco.getLON(), lon);

            resultado = db.Insert(CriaBanco.getTABELA(), null, valores);
            db.Close();

            if (resultado == -1)
                //return "Erro ao inserir registro";
                return false;

            return true;
            //else
            //return "Registro Inserido com sucesso";
            //return "Erro ao inserir registro";

        }
    }
}