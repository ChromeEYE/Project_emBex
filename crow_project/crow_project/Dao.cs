﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace crow_project {
    public class Dao {
        //コネクション変数
        private SqlConnection con;

        //トランザクション変数
        private SqlTransaction trn;

        /// <summary>
        /// コンストラクタで各変数をインスタンス化
        /// </summary>
        public Dao() {
            con = TranMng.Transaction.Connection;
            trn = TranMng.Transaction;
        }

        /// <summary>
        /// データベースの従業員マスタから従業員情報、所属マスタから所属部署名を返す
        /// </summary>
        /// <returns></returns>
        public List<string> Show() {
            //string型Listのバッファ
            List<string> rtnArgs = new List<string>();

            //sqlコマンドでselectし、従業員マスタの全情報を取得・buffargsに代入
            using (SqlCommand cmd = new SqlCommand("SELECT emp_cd, last_nm, first_nm, last_nm_kana, first_nm_kana, gender_nm, birth_date, section_nm, emp_date FROM m_employee INNER JOIN m_section ON m_employee.section_cd = m_section.section_cd INNER JOIN m_gender ON m_employee.gender_cd = m_gender.gender_cd", con, trn)) {
                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        rtnArgs.Add(reader["emp_cd"].ToString());
                        rtnArgs.Add(reader["last_nm"].ToString());
                        rtnArgs.Add(reader["first_nm"].ToString());
                        rtnArgs.Add(reader["last_nm_kana"].ToString());
                        rtnArgs.Add(reader["first_nm_kana"].ToString());
                        rtnArgs.Add(reader["gender_nm"].ToString());
                        rtnArgs.Add(DateTime.Parse(reader["birth_date"].ToString()).ToShortDateString());
                        rtnArgs.Add(reader["section_nm"].ToString());
                        rtnArgs.Add(DateTime.Parse(reader["emp_date"].ToString()).ToShortDateString());
                    }
                }
            }
            return rtnArgs;
        }

        /// <summary>
        /// 渡された従業員コードに合致する従業員データを削除する
        /// </summary>
        /// <param name="cd">従業員コード</param>
        /// <returns></returns>
        public bool Delete(string cd) {
            bool rtn = false;

            int execute = 0;

            //sqlコマンドでdeleteを実行
            using (SqlCommand cmd = new SqlCommand("DELETE FROM m_employee WHERE emp_cd = @code", con, trn)) {
                cmd.Parameters.Add("@code", SqlDbType.NVarChar).Value = cd;

                execute = cmd.ExecuteNonQuery();
            }
            //変更行数があった場合返り値をtrueに
            if (execute != 0)
                rtn = true;

            return rtn;
        }

        /// <summary>
        /// コレクションをテーブルに
        /// </summary>
        /// <param name="employeeData"></param>
        /// <returns></returns>
        public bool Insert(Dictionary<string, string> employeeData) {
            bool rtn = false;

            int execute = 0;

            try {
                //sqlcommandでinsert処理を実行
                using (SqlCommand cmd = new SqlCommand("INSERT INTO m_employee VALUES(@code, @lastName, @firstName, @lastNmKana, @firstNmKana, @gender, @birthDay, @section, @date, @createdate, @update)", con, trn)) {
                    cmd.Parameters.Add("@code", SqlDbType.Char).Value = employeeData["従業員コード"];
                    cmd.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = employeeData["氏"];
                    cmd.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = employeeData["名"];
                    cmd.Parameters.Add("@lastNmKana", SqlDbType.NVarChar).Value = employeeData["氏（フリガナ）"];
                    cmd.Parameters.Add("@firstNmKana", SqlDbType.NVarChar).Value = employeeData["名（フリガナ）"];
                    cmd.Parameters.Add("@gender", SqlDbType.NVarChar).Value = employeeData["性別コード"];
                    cmd.Parameters.Add("@birthDay", SqlDbType.Date).Value = employeeData["生年月日"];
                    cmd.Parameters.Add("@section", SqlDbType.Char).Value = employeeData["所属コード"];
                    cmd.Parameters.Add("@date", SqlDbType.Date).Value = employeeData["入社日"];
                    cmd.Parameters.Add("@createdate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@update", SqlDbType.DateTime).Value = DateTime.Now;

                    execute = cmd.ExecuteNonQuery();
                }
            } catch {

            }
            //見つかった場合返り値をtureに
            if (execute != 0)
                rtn = true;

            return rtn;
        }

        /// <summary>
        /// 文字列をセレクトし、有効な文字列が返ってきた場合ログイン
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool Login(string UserID, string Password) {
            bool rtn = false;

            string executeID = "", executePW = "";

            //sqlcommandでIDとパスワードを捜索
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM m_user WHERE user_id = @ID AND password = @password", con, trn)) {
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = UserID;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = Password;
                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        executeID = reader["user_id"].ToString();
                        executePW = reader["password"].ToString();
                    }
                }
            }

            //見つかった場合返り値をtrueに
            if (!string.IsNullOrEmpty(executeID) || !string.IsNullOrEmpty(executePW))
                rtn = true;

            return rtn;
        }
    }
}