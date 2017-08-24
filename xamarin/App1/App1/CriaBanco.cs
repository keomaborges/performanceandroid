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
using Java.Lang;

namespace App1
{
    public class CriaBanco : SQLiteOpenHelper
    {
        private static string NOME_BANCO = "banco.db";
        private static string TABELA = "coordenadas";
        private static string ID = "_id";
        private static string LAT = "lat";
        private static string LON = "lon";
        private static int VERSAO = 1;

        public static string getLAT()
        {
            return LAT;
        }

        public static string getLON()
        {
            return LON;
        }

        public static string getTABELA()
        {
            return TABELA;
        }

        public CriaBanco(Context context)
        {
            base(context, NOME_BANCO, null, VERSAO);
        }

        public void onCreate(SQLiteDatabase db)
        {
            string sql = "CREATE TABLE " + TABELA + " ("
                    + ID + " integer primary key autoincrement, "
                    + LAT + " text, "
                    + LON + " text "
                    + " )";
            db.ExecSQL(sql);
        }

        public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.execSQL("DROP TABLE IF EXISTS" + TABELA);
            onCreate(db);
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            throw new NotImplementedException();
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            throw new NotImplementedException();
        }
    }
}