using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace Wordz.DB {
    public class Database : Component {
        private Container components = null;

        public static string SoundPath = @"D:\Test\LearnWords\Src\Wordz\Wordz.DB\res\sound\";
        public static string ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=wordz;Integrated Security=True";
        public static bool UseSoundFiles = true;
        public static bool TranscriptRussianWords = false;
        public static bool AlwaysCreateNewInstance = true;
        private static Database instance;
        private static SqlTransaction transaction;
        public SqlConnection sqlConnection;
        public SqlCommand pr_word_get_by_original;
        public SqlCommand pr_account_word_get;
        public SqlCommand pr_account_word_ins;
        public SqlCommand pr_account_get;
        public SqlCommand pr_account_ins;
        public SqlCommand pr_account_upd;
        public SqlCommand pr_word_get_sound_by_id;
        public SqlCommand pr_domain_get;
        public SqlCommand pr_word_get_by_domain;
        public SqlCommand pr_word_get_by_random;
        public SqlCommand pr_account_word_get_count;
        public SqlCommand pr_account_word_get_list;
        public SqlCommand pr_account_word_del;
        public SqlCommand pr_word_get_count_sounded;
        public SqlCommand pr_word_ins;
        public SqlCommand pr_article_get;
        public SqlCommand pr_category_get;
        public SqlCommand pr_site_get;
        public SqlCommand pr_article_get_by_id;
        public SqlCommand pr_account_get_count;
        public SqlCommand pr_account_verb_del_list;
        public SqlCommand pr_account_verb_ins;
        public SqlCommand pr_account_verb_get_info;
        public SqlCommand pr_verb_type_get_list;
        public SqlCommand pr_verb_get_list;
        public SqlCommand pr_word_get_by_id;
        public SqlCommand pr_account_verb_get_top;
        public SqlCommand pr_account_word_get_top;
        public SqlCommand pr_word_quiz_get_top;
        public SqlCommand pr_word_quiz_get_place;
        public SqlCommand pr_word_order_quiz_get_top;
        public SqlCommand pr_word_order_quiz_get_place;
        public SqlCommand pr_word_order_get_random;
        public SqlCommand pr_word_get_list_unsounded_short;
        public SqlCommand pr_film_clear_all;
        public SqlCommand pr_film_part_ins;
        public SqlCommand pr_film_ins;
        public SqlCommand pr_film_category_get_list;
        public SqlCommand pr_film_get_list_by_category;
        public SqlCommand pr_film_get_list_by_search;
		public SqlCommand pr_film_get;
		public SqlCommand pr_language_get_list;
        public SqlCommand pr_word_upd;
        public SqlCommand pr_word_get_id_by_text;
        public SqlCommand pr_tv_get_list;
        public SqlCommand pr_tv_get;
        public SqlCommand pr_fm_get_list;
        public SqlCommand pr_fm_get;
        public SqlCommand pr_word_get_by_ordered;
        public SqlCommand pr_word_get_count;
        public SqlCommand pr_word_ins_use_exist;
		public SqlCommand pr_level_get_random;
		public SqlCommand pr_level_quiz_get_place;
        public SqlCommand pr_film_upd_status;
        public SqlCommand pr_course_details_get;
        public SqlCommand pr_course_update;
        public SqlCommand pr_modules_for_course_lst;
        public SqlCommand pr_module_get;
        public SqlCommand pr_rating_update;
        public SqlCommand pr_course_get_list;
        public SqlCommand pr_picture_get;
        public SqlCommand pr_user_comment_add;
        public SqlCommand pr_user_comment_lst;
        public SqlCommand pr_payment_for_course_add;
        public SqlCommand pr_exercise_text_get;
        public SqlCommand pr_exercise_text_list;
        public SqlCommand pr_exercise_text_ins;
        public SqlCommand pr_exercise_text_upd;
        public SqlCommand pr_exercise_text_del;
        public SqlCommand pr_exercise_select_get;
        public SqlCommand pr_exercise_select_answer_list;
        public SqlCommand pr_exercise_select_ins;
        public SqlCommand pr_exercise_select_upd;
        public SqlCommand pr_exercise_select_del;
        public SqlCommand pr_answer_ins;
        public SqlCommand pr_answer_upd;
        public SqlCommand pr_answer_del;
        public SqlCommand pr_payment_for_module_add;
        public SqlCommand pr_account_money_balance_lst;
        public SqlCommand pr_picture_update;
        public SqlCommand pr_currency_lst;
        public SqlCommand pr_exercises_for_module_lst;
        public SqlCommand pr_module_update;
        public SqlCommand pr_exercise_text_answer_get;
        public SqlCommand pr_exercise_text_answer_ins;
        public SqlCommand pr_exercise_text_answer_upd;
        public SqlCommand pr_exercise_answer_text_get;
        public SqlCommand pr_exercise_answer_text_ins;
        public SqlCommand pr_exercise_answer_text_upd;
        public SqlCommand pr_exercise_select_text_get;
        public SqlCommand pr_exercise_select_text_ins;
        public SqlCommand pr_exercise_select_text_upd;
        public SqlCommand pr_exercise_skip_text_get;
        public SqlCommand pr_exercise_skip_text_ins;
        public SqlCommand pr_exercise_skip_text_upd;
        public SqlCommand pr_tv_get_other_list;
        public SqlCommand pr_tv_update_or_insert;
        public SqlCommand pr_course_approve;
        public SqlCommand pr_tv_delete;
        public SqlCommand pr_tv_update_order;
        public SqlCommand pr_modules_for_course_order;
        public SqlCommand pr_exercises_for_module_order;
        public SqlCommand pr_module_delete;
        public SqlCommand pr_fm_delete;
        public SqlCommand pr_fm_get_other_list;
        public SqlCommand pr_fm_update_or_insert;
        public SqlCommand pr_fm_update_order;
        public SqlCommand pr_user_course_password_check_and_update;
        public SqlCommand pr_user_comment_items_count;
        public SqlCommand pr_user_comment_claim;
        public SqlCommand pr_user_comment_rate;
        public SqlCommand pr_film_get_list;
        public SqlCommand pr_film_get_other_list;
        public SqlCommand pr_film_update_or_insert;
        public SqlCommand pr_film_update_order;
        public SqlCommand pr_film_delete;
		public SqlCommand pr_level_quiz_get_top;

        private SqlCommand[] commands {
            get {
                return
                    new SqlCommand[] {
                        pr_word_get_count,
                        pr_word_get_by_original,
                        pr_account_word_get,
                        pr_account_word_ins,
                        pr_account_get,
                        pr_account_ins,
                        pr_account_upd,
                        pr_word_get_sound_by_id,
                        pr_domain_get,
                        pr_word_get_by_domain,
                        pr_word_get_by_random,
                        pr_word_get_by_ordered,
                        pr_account_word_get_count,
                        pr_account_word_get_list,
                        pr_account_word_del,
                        pr_word_get_count_sounded,
                        pr_word_ins,
                        pr_article_get,
                        pr_category_get,
                        pr_site_get,
                        pr_account_get_count,
                        pr_article_get_by_id,
                        pr_verb_get_list,
                        pr_account_verb_del_list,
                        pr_account_verb_ins,
                        pr_account_verb_get_info,
                        pr_verb_type_get_list,
                        pr_word_get_by_id,
                        pr_account_word_get_top,
                        pr_account_verb_get_top,
                        pr_word_quiz_get_top,
                        pr_word_quiz_get_place,
                        pr_word_order_quiz_get_top,
                        pr_word_order_quiz_get_place,
                        pr_word_order_get_random,
                        pr_word_get_list_unsounded_short,
                        pr_film_clear_all,
                        pr_film_part_ins,
                        pr_film_ins,
                        pr_film_category_get_list,
                        pr_film_get_list_by_category,
                        pr_film_get_list_by_search,
                        pr_film_get,
						pr_film_upd_status,
                        pr_language_get_list,
                        pr_word_upd,
                        pr_word_get_id_by_text,
                        pr_tv_get_list,
                        pr_tv_get,
                        pr_fm_get_list,
                        pr_fm_get,
						pr_word_ins_use_exist,
						pr_level_get_random,
						pr_level_quiz_get_place,
						pr_level_quiz_get_top,
                        pr_course_details_get,
                        pr_course_update,
                        pr_modules_for_course_lst,
                        pr_module_get,
                        pr_rating_update,
                        pr_picture_get,
                        pr_user_comment_add,
                        pr_user_comment_lst,
                        pr_payment_for_course_add,
                        pr_payment_for_module_add,
                        pr_account_money_balance_lst,
                        pr_picture_update,
                        pr_exercises_for_module_lst,
                        pr_module_update,
                        pr_tv_delete,
                        pr_tv_update_or_insert,
                        pr_tv_update_order,
                        pr_modules_for_course_order,
                        pr_exercises_for_module_order,
                        pr_module_delete,
                        pr_film_get_list,
                        pr_film_update_or_insert
				};
            }
        }
        
        public static Database GetInstance() {
            if (AlwaysCreateNewInstance) {
                return new Database();
            } else {
                if (instance == null) {
                    instance = new Database();
                }
                if (instance.sqlConnection.State != ConnectionState.Open) {
                    instance.sqlConnection.Open();
                }
                return instance;
            }
        }

        private void OpenConnection() {
            sqlConnection.Open();
        }

        public void CloseConnection() {
            if (AlwaysCreateNewInstance) {
                sqlConnection.Close();
            }
        }

        public void BeginTransaction() {
            transaction = sqlConnection.BeginTransaction();
            foreach (SqlCommand command in commands) {
                command.Transaction = transaction;
            }
        }

        public void RollbackTransaction() {
            transaction.Rollback();
            foreach (SqlCommand command in commands) {
                command.Transaction = null;
            }
        }

        public Database() {
            InitializeComponent();
            sqlConnection.ConnectionString = ConnectionString;
            OpenConnection();
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent() {
            this.sqlConnection = new System.Data.SqlClient.SqlConnection();
            this.pr_word_get_count = new System.Data.SqlClient.SqlCommand();
            this.pr_word_get_by_original = new System.Data.SqlClient.SqlCommand();
            this.pr_account_word_get = new System.Data.SqlClient.SqlCommand();
            this.pr_account_word_ins = new System.Data.SqlClient.SqlCommand();
            this.pr_account_get = new System.Data.SqlClient.SqlCommand();
            this.pr_account_ins = new System.Data.SqlClient.SqlCommand();
            this.pr_account_upd = new System.Data.SqlClient.SqlCommand();
            this.pr_word_get_sound_by_id = new System.Data.SqlClient.SqlCommand();
            this.pr_domain_get = new System.Data.SqlClient.SqlCommand();
            this.pr_word_get_by_domain = new System.Data.SqlClient.SqlCommand();
            this.pr_word_get_by_random = new System.Data.SqlClient.SqlCommand();
            this.pr_word_get_by_ordered = new System.Data.SqlClient.SqlCommand();
            this.pr_account_word_get_count = new System.Data.SqlClient.SqlCommand();
            this.pr_account_word_get_list = new System.Data.SqlClient.SqlCommand();
            this.pr_account_word_del = new System.Data.SqlClient.SqlCommand();
            this.pr_word_get_count_sounded = new System.Data.SqlClient.SqlCommand();
            this.pr_word_ins = new System.Data.SqlClient.SqlCommand();
            this.pr_article_get = new System.Data.SqlClient.SqlCommand();
            this.pr_category_get = new System.Data.SqlClient.SqlCommand();
            this.pr_site_get = new System.Data.SqlClient.SqlCommand();
            this.pr_article_get_by_id = new System.Data.SqlClient.SqlCommand();
            this.pr_account_get_count = new System.Data.SqlClient.SqlCommand();
            this.pr_account_verb_del_list = new System.Data.SqlClient.SqlCommand();
            this.pr_account_verb_ins = new System.Data.SqlClient.SqlCommand();
            this.pr_account_verb_get_info = new System.Data.SqlClient.SqlCommand();
            this.pr_verb_type_get_list = new System.Data.SqlClient.SqlCommand();
            this.pr_verb_get_list = new System.Data.SqlClient.SqlCommand();
            this.pr_word_get_by_id = new System.Data.SqlClient.SqlCommand();
            this.pr_account_verb_get_top = new System.Data.SqlClient.SqlCommand();
            this.pr_account_word_get_top = new System.Data.SqlClient.SqlCommand();
            this.pr_word_quiz_get_top = new System.Data.SqlClient.SqlCommand();
            this.pr_word_quiz_get_place = new System.Data.SqlClient.SqlCommand();
            this.pr_word_order_quiz_get_top = new System.Data.SqlClient.SqlCommand();
            this.pr_word_order_quiz_get_place = new System.Data.SqlClient.SqlCommand();
            this.pr_word_order_get_random = new System.Data.SqlClient.SqlCommand();
            this.pr_word_get_list_unsounded_short = new System.Data.SqlClient.SqlCommand();
            this.pr_film_clear_all = new System.Data.SqlClient.SqlCommand();
            this.pr_film_part_ins = new System.Data.SqlClient.SqlCommand();
            this.pr_film_ins = new System.Data.SqlClient.SqlCommand();
            this.pr_film_category_get_list = new System.Data.SqlClient.SqlCommand();
            this.pr_film_get_list_by_category = new System.Data.SqlClient.SqlCommand();
            this.pr_film_get_list_by_search = new System.Data.SqlClient.SqlCommand();
            this.pr_film_get = new System.Data.SqlClient.SqlCommand();
            this.pr_language_get_list = new System.Data.SqlClient.SqlCommand();
            this.pr_word_upd = new System.Data.SqlClient.SqlCommand();
            this.pr_word_get_id_by_text = new System.Data.SqlClient.SqlCommand();
            this.pr_tv_get_list = new System.Data.SqlClient.SqlCommand();
            this.pr_tv_get = new System.Data.SqlClient.SqlCommand();
            this.pr_fm_get_list = new System.Data.SqlClient.SqlCommand();
            this.pr_fm_get = new System.Data.SqlClient.SqlCommand();
            this.pr_word_ins_use_exist = new System.Data.SqlClient.SqlCommand();
            this.pr_level_get_random = new System.Data.SqlClient.SqlCommand();
            this.pr_level_quiz_get_place = new System.Data.SqlClient.SqlCommand();
            this.pr_level_quiz_get_top = new System.Data.SqlClient.SqlCommand();
            this.pr_film_upd_status = new System.Data.SqlClient.SqlCommand();
            this.pr_course_details_get = new System.Data.SqlClient.SqlCommand();
            this.pr_course_update = new System.Data.SqlClient.SqlCommand();
            this.pr_modules_for_course_lst = new System.Data.SqlClient.SqlCommand();
            this.pr_module_get = new System.Data.SqlClient.SqlCommand();
            this.pr_rating_update = new System.Data.SqlClient.SqlCommand();
            this.pr_course_get_list = new System.Data.SqlClient.SqlCommand();
            this.pr_picture_get = new System.Data.SqlClient.SqlCommand();
            this.pr_user_comment_add = new System.Data.SqlClient.SqlCommand();
            this.pr_user_comment_lst = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_text_get = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_text_list = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_text_ins = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_text_upd = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_text_del = new System.Data.SqlClient.SqlCommand();
            this.pr_payment_for_course_add = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_select_get = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_select_answer_list = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_select_ins = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_select_upd = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_select_del = new System.Data.SqlClient.SqlCommand();
            this.pr_answer_ins = new System.Data.SqlClient.SqlCommand();
            this.pr_answer_upd = new System.Data.SqlClient.SqlCommand();
            this.pr_answer_del = new System.Data.SqlClient.SqlCommand();
            this.pr_payment_for_module_add = new System.Data.SqlClient.SqlCommand();
            this.pr_account_money_balance_lst = new System.Data.SqlClient.SqlCommand();
            this.pr_picture_update = new System.Data.SqlClient.SqlCommand();
            this.pr_currency_lst = new System.Data.SqlClient.SqlCommand();
            this.pr_exercises_for_module_lst = new System.Data.SqlClient.SqlCommand();
            this.pr_module_update = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_text_answer_get = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_text_answer_ins = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_text_answer_upd = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_answer_text_get = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_answer_text_ins = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_answer_text_upd = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_select_text_get = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_select_text_ins = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_select_text_upd = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_skip_text_get = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_skip_text_ins = new System.Data.SqlClient.SqlCommand();
            this.pr_exercise_skip_text_upd = new System.Data.SqlClient.SqlCommand();
            this.pr_tv_get_other_list = new System.Data.SqlClient.SqlCommand();
            this.pr_tv_update_or_insert = new System.Data.SqlClient.SqlCommand();
            this.pr_course_approve = new System.Data.SqlClient.SqlCommand();
            this.pr_tv_delete = new System.Data.SqlClient.SqlCommand();
            this.pr_tv_update_order = new System.Data.SqlClient.SqlCommand();
            this.pr_modules_for_course_order = new System.Data.SqlClient.SqlCommand();
            this.pr_exercises_for_module_order = new System.Data.SqlClient.SqlCommand();
            this.pr_module_delete = new System.Data.SqlClient.SqlCommand();
            this.pr_fm_delete = new System.Data.SqlClient.SqlCommand();
            this.pr_fm_get_other_list = new System.Data.SqlClient.SqlCommand();
            this.pr_fm_update_or_insert = new System.Data.SqlClient.SqlCommand();
            this.pr_fm_update_order = new System.Data.SqlClient.SqlCommand();
            this.pr_user_course_password_check_and_update = new System.Data.SqlClient.SqlCommand();
            this.pr_user_comment_items_count = new System.Data.SqlClient.SqlCommand();
            this.pr_user_comment_claim = new System.Data.SqlClient.SqlCommand();
            this.pr_user_comment_rate = new System.Data.SqlClient.SqlCommand();
            this.pr_film_get_list = new System.Data.SqlClient.SqlCommand();
            this.pr_film_get_other_list = new System.Data.SqlClient.SqlCommand();
            this.pr_film_update_or_insert = new System.Data.SqlClient.SqlCommand();
            this.pr_film_update_order = new System.Data.SqlClient.SqlCommand();
            this.pr_film_delete = new System.Data.SqlClient.SqlCommand();
            // 
            // sqlConnection
            // 
            this.sqlConnection.ConnectionString = "Data Source=.\\sqlexpress;Initial Catalog=wordz;Integrated Security=True;Pooling=F" +
    "alse";
            this.sqlConnection.FireInfoMessageEventOnUserErrors = false;
            // 
            // pr_word_get_count
            // 
            this.pr_word_get_count.CommandText = "[pr_word_get_count]";
            this.pr_word_get_count.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_word_get_count.Connection = this.sqlConnection;
            this.pr_word_get_count.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_word_get_by_original
            // 
            this.pr_word_get_by_original.CommandText = "[pr_word_get_by_original]";
            this.pr_word_get_by_original.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_word_get_by_original.Connection = this.sqlConnection;
            this.pr_word_get_by_original.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@original", System.Data.SqlDbType.NVarChar, 254),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_account_word_get
            // 
            this.pr_account_word_get.CommandText = "[pr_account_word_get]";
            this.pr_account_word_get.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_account_word_get.Connection = this.sqlConnection;
            this.pr_account_word_get.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@word_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_account_word_ins
            // 
            this.pr_account_word_ins.CommandText = "[pr_account_word_ins]";
            this.pr_account_word_ins.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_account_word_ins.Connection = this.sqlConnection;
            this.pr_account_word_ins.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@word_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_account_get
            // 
            this.pr_account_get.CommandText = "[pr_account_get]";
            this.pr_account_get.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_account_get.Connection = this.sqlConnection;
            this.pr_account_get.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@nick", System.Data.SqlDbType.NVarChar, 20),
            new System.Data.SqlClient.SqlParameter("@password", System.Data.SqlDbType.NVarChar, 20)});
            // 
            // pr_account_ins
            // 
            this.pr_account_ins.CommandText = "[pr_account_ins]";
            this.pr_account_ins.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_account_ins.Connection = this.sqlConnection;
            this.pr_account_ins.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@nick", System.Data.SqlDbType.NVarChar, 20),
            new System.Data.SqlClient.SqlParameter("@email", System.Data.SqlDbType.NVarChar, 50),
            new System.Data.SqlClient.SqlParameter("@password", System.Data.SqlDbType.NVarChar, 20)});
            // 
            // pr_account_upd
            // 
            this.pr_account_upd.CommandText = "[pr_account_upd]";
            this.pr_account_upd.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_account_upd.Connection = this.sqlConnection;
            this.pr_account_upd.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@nick", System.Data.SqlDbType.NVarChar, 20),
            new System.Data.SqlClient.SqlParameter("@email", System.Data.SqlDbType.NVarChar, 50),
            new System.Data.SqlClient.SqlParameter("@password", System.Data.SqlDbType.NVarChar, 20)});
            // 
            // pr_word_get_sound_by_id
            // 
            this.pr_word_get_sound_by_id.CommandText = "[pr_word_get_sound_by_id]";
            this.pr_word_get_sound_by_id.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_word_get_sound_by_id.Connection = this.sqlConnection;
            this.pr_word_get_sound_by_id.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_domain_get
            // 
            this.pr_domain_get.CommandText = "[pr_domain_get]";
            this.pr_domain_get.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_domain_get.Connection = this.sqlConnection;
            this.pr_domain_get.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_word_get_by_domain
            // 
            this.pr_word_get_by_domain.CommandText = "[pr_word_get_by_domain]";
            this.pr_word_get_by_domain.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_word_get_by_domain.Connection = this.sqlConnection;
            this.pr_word_get_by_domain.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@domain_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@word_count", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@word_start_index", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_word_get_by_random
            // 
            this.pr_word_get_by_random.CommandText = "[pr_word_get_by_random]";
            this.pr_word_get_by_random.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_word_get_by_random.Connection = this.sqlConnection;
            this.pr_word_get_by_random.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@word_count", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_word_get_by_ordered
            // 
            this.pr_word_get_by_ordered.CommandText = "[pr_word_get_by_ordered]";
            this.pr_word_get_by_ordered.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_word_get_by_ordered.Connection = this.sqlConnection;
            this.pr_word_get_by_ordered.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@word_count", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@word_start_index", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_account_word_get_count
            // 
            this.pr_account_word_get_count.CommandText = "[pr_account_word_get_count]";
            this.pr_account_word_get_count.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_account_word_get_count.Connection = this.sqlConnection;
            this.pr_account_word_get_count.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_account_word_get_list
            // 
            this.pr_account_word_get_list.CommandText = "[pr_account_word_get_list]";
            this.pr_account_word_get_list.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_account_word_get_list.Connection = this.sqlConnection;
            this.pr_account_word_get_list.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_account_word_del
            // 
            this.pr_account_word_del.CommandText = "[pr_account_word_del]";
            this.pr_account_word_del.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_account_word_del.Connection = this.sqlConnection;
            this.pr_account_word_del.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@word_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_word_get_count_sounded
            // 
            this.pr_word_get_count_sounded.CommandText = "[pr_word_get_count_sounded]";
            this.pr_word_get_count_sounded.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_word_get_count_sounded.Connection = this.sqlConnection;
            this.pr_word_get_count_sounded.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_word_ins
            // 
            this.pr_word_ins.CommandText = "[pr_word_ins]";
            this.pr_word_ins.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_word_ins.Connection = this.sqlConnection;
            this.pr_word_ins.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@original", System.Data.SqlDbType.NVarChar, 254),
            new System.Data.SqlClient.SqlParameter("@translation", System.Data.SqlDbType.NVarChar, 254),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_article_get
            // 
            this.pr_article_get.CommandText = "[pr_article_get]";
            this.pr_article_get.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_article_get.Connection = this.sqlConnection;
            this.pr_article_get.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_category_get
            // 
            this.pr_category_get.CommandText = "[pr_category_get]";
            this.pr_category_get.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_category_get.Connection = this.sqlConnection;
            this.pr_category_get.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_site_get
            // 
            this.pr_site_get.CommandText = "[pr_site_get]";
            this.pr_site_get.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_site_get.Connection = this.sqlConnection;
            this.pr_site_get.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_article_get_by_id
            // 
            this.pr_article_get_by_id.CommandText = "[pr_article_get_by_id]";
            this.pr_article_get_by_id.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_article_get_by_id.Connection = this.sqlConnection;
            this.pr_article_get_by_id.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_account_get_count
            // 
            this.pr_account_get_count.CommandText = "[pr_account_get_count]";
            this.pr_account_get_count.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_account_get_count.Connection = this.sqlConnection;
            this.pr_account_get_count.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_account_verb_del_list
            // 
            this.pr_account_verb_del_list.CommandText = "[pr_account_verb_del_list]";
            this.pr_account_verb_del_list.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_account_verb_del_list.Connection = this.sqlConnection;
            this.pr_account_verb_del_list.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_account_verb_ins
            // 
            this.pr_account_verb_ins.CommandText = "[pr_account_verb_ins]";
            this.pr_account_verb_ins.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_account_verb_ins.Connection = this.sqlConnection;
            this.pr_account_verb_ins.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@verb_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_account_verb_get_info
            // 
            this.pr_account_verb_get_info.CommandText = "[pr_account_verb_get_info]";
            this.pr_account_verb_get_info.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_account_verb_get_info.Connection = this.sqlConnection;
            this.pr_account_verb_get_info.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_verb_type_get_list
            // 
            this.pr_verb_type_get_list.CommandText = "[pr_verb_type_get_list]";
            this.pr_verb_type_get_list.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_verb_type_get_list.Connection = this.sqlConnection;
            this.pr_verb_type_get_list.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_verb_get_list
            // 
            this.pr_verb_get_list.CommandText = "[pr_verb_get_list]";
            this.pr_verb_get_list.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_verb_get_list.Connection = this.sqlConnection;
            this.pr_verb_get_list.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@load_popular", System.Data.SqlDbType.Bit, 1),
            new System.Data.SqlClient.SqlParameter("@not_use_well_known_verbs", System.Data.SqlDbType.Bit, 1),
            new System.Data.SqlClient.SqlParameter("@word_count", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_word_get_by_id
            // 
            this.pr_word_get_by_id.CommandText = "[pr_word_get_by_id]";
            this.pr_word_get_by_id.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_word_get_by_id.Connection = this.sqlConnection;
            this.pr_word_get_by_id.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_account_verb_get_top
            // 
            this.pr_account_verb_get_top.CommandText = "[pr_account_verb_get_top]";
            this.pr_account_verb_get_top.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_account_verb_get_top.Connection = this.sqlConnection;
            this.pr_account_verb_get_top.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_account_word_get_top
            // 
            this.pr_account_word_get_top.CommandText = "[pr_account_word_get_top]";
            this.pr_account_word_get_top.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_account_word_get_top.Connection = this.sqlConnection;
            this.pr_account_word_get_top.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_word_quiz_get_top
            // 
            this.pr_word_quiz_get_top.CommandText = "[pr_word_quiz_get_top]";
            this.pr_word_quiz_get_top.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_word_quiz_get_top.Connection = this.sqlConnection;
            this.pr_word_quiz_get_top.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_word_quiz_get_place
            // 
            this.pr_word_quiz_get_place.CommandText = "[pr_word_quiz_get_place]";
            this.pr_word_quiz_get_place.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_word_quiz_get_place.Connection = this.sqlConnection;
            this.pr_word_quiz_get_place.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@nick", System.Data.SqlDbType.NVarChar, 20),
            new System.Data.SqlClient.SqlParameter("@success_count", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_word_order_quiz_get_top
            // 
            this.pr_word_order_quiz_get_top.CommandText = "[pr_word_order_quiz_get_top]";
            this.pr_word_order_quiz_get_top.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_word_order_quiz_get_top.Connection = this.sqlConnection;
            this.pr_word_order_quiz_get_top.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_word_order_quiz_get_place
            // 
            this.pr_word_order_quiz_get_place.CommandText = "[pr_word_order_quiz_get_place]";
            this.pr_word_order_quiz_get_place.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_word_order_quiz_get_place.Connection = this.sqlConnection;
            this.pr_word_order_quiz_get_place.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@nick", System.Data.SqlDbType.NVarChar, 20),
            new System.Data.SqlClient.SqlParameter("@success_count", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_word_order_get_random
            // 
            this.pr_word_order_get_random.CommandText = "[pr_word_order_get_random]";
            this.pr_word_order_get_random.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_word_order_get_random.Connection = this.sqlConnection;
            this.pr_word_order_get_random.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_word_get_list_unsounded_short
            // 
            this.pr_word_get_list_unsounded_short.CommandText = "[pr_word_get_list_unsounded_short]";
            this.pr_word_get_list_unsounded_short.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_word_get_list_unsounded_short.Connection = this.sqlConnection;
            this.pr_word_get_list_unsounded_short.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_film_clear_all
            // 
            this.pr_film_clear_all.CommandText = "[pr_film_clear_all]";
            this.pr_film_clear_all.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_film_clear_all.Connection = this.sqlConnection;
            this.pr_film_clear_all.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_film_part_ins
            // 
            this.pr_film_part_ins.CommandText = "[pr_film_part_ins]";
            this.pr_film_part_ins.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_film_part_ins.Connection = this.sqlConnection;
            this.pr_film_part_ins.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@url", System.Data.SqlDbType.NVarChar, 100),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@number", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_film_ins
            // 
            this.pr_film_ins.CommandText = "[pr_film_ins]";
            this.pr_film_ins.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_film_ins.Connection = this.sqlConnection;
            this.pr_film_ins.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.NVarChar, 100),
            new System.Data.SqlClient.SqlParameter("@category", System.Data.SqlDbType.NVarChar, 30),
            new System.Data.SqlClient.SqlParameter("@player_pattern", System.Data.SqlDbType.NVarChar, 2000),
            new System.Data.SqlClient.SqlParameter("@url", System.Data.SqlDbType.NVarChar, 100),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_film_category_get_list
            // 
            this.pr_film_category_get_list.CommandText = "[pr_film_category_get_list]";
            this.pr_film_category_get_list.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_film_category_get_list.Connection = this.sqlConnection;
            this.pr_film_category_get_list.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_film_get_list_by_category
            // 
            this.pr_film_get_list_by_category.CommandText = "[pr_film_get_list_by_category]";
            this.pr_film_get_list_by_category.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_film_get_list_by_category.Connection = this.sqlConnection;
            this.pr_film_get_list_by_category.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@category_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_film_get_list_by_search
            // 
            this.pr_film_get_list_by_search.CommandText = "[pr_film_get_list_by_search]";
            this.pr_film_get_list_by_search.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_film_get_list_by_search.Connection = this.sqlConnection;
            this.pr_film_get_list_by_search.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@search", System.Data.SqlDbType.NVarChar, 10),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_film_get
            // 
            this.pr_film_get.CommandText = "[pr_film_get]";
            this.pr_film_get.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_film_get.Connection = this.sqlConnection;
            this.pr_film_get.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_language_get_list
            // 
            this.pr_language_get_list.CommandText = "[pr_language_get_list]";
            this.pr_language_get_list.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_language_get_list.Connection = this.sqlConnection;
            this.pr_language_get_list.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_word_upd
            // 
            this.pr_word_upd.CommandText = "[pr_word_upd]";
            this.pr_word_upd.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_word_upd.Connection = this.sqlConnection;
            this.pr_word_upd.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@original", System.Data.SqlDbType.VarChar, 254),
            new System.Data.SqlClient.SqlParameter("@translation", System.Data.SqlDbType.VarChar, 254),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_word_get_id_by_text
            // 
            this.pr_word_get_id_by_text.CommandText = "[pr_word_get_id_by_text]";
            this.pr_word_get_id_by_text.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_word_get_id_by_text.Connection = this.sqlConnection;
            this.pr_word_get_id_by_text.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@text", System.Data.SqlDbType.NVarChar, 254),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_tv_get_list
            // 
            this.pr_tv_get_list.CommandText = "[pr_tv_get_list]";
            this.pr_tv_get_list.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_tv_get_list.Connection = this.sqlConnection;
            this.pr_tv_get_list.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_tv_get
            // 
            this.pr_tv_get.CommandText = "[pr_tv_get]";
            this.pr_tv_get.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_tv_get.Connection = this.sqlConnection;
            this.pr_tv_get.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_fm_get_list
            // 
            this.pr_fm_get_list.CommandText = "[pr_fm_get_list]";
            this.pr_fm_get_list.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_fm_get_list.Connection = this.sqlConnection;
            this.pr_fm_get_list.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_fm_get
            // 
            this.pr_fm_get.CommandText = "[pr_fm_get]";
            this.pr_fm_get.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_fm_get.Connection = this.sqlConnection;
            this.pr_fm_get.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_word_ins_use_exist
            // 
            this.pr_word_ins_use_exist.CommandText = "[pr_word_ins_use_exist]";
            this.pr_word_ins_use_exist.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_word_ins_use_exist.Connection = this.sqlConnection;
            this.pr_word_ins_use_exist.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@original", System.Data.SqlDbType.NVarChar, 254),
            new System.Data.SqlClient.SqlParameter("@translation", System.Data.SqlDbType.NVarChar, 254),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_level_get_random
            // 
            this.pr_level_get_random.CommandText = "[pr_level_get_random]";
            this.pr_level_get_random.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_level_get_random.Connection = this.sqlConnection;
            this.pr_level_get_random.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_level_quiz_get_place
            // 
            this.pr_level_quiz_get_place.CommandText = "[pr_level_quiz_get_place]";
            this.pr_level_quiz_get_place.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_level_quiz_get_place.Connection = this.sqlConnection;
            this.pr_level_quiz_get_place.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@nick", System.Data.SqlDbType.NVarChar, 20),
            new System.Data.SqlClient.SqlParameter("@success_count", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_level_quiz_get_top
            // 
            this.pr_level_quiz_get_top.CommandText = "[pr_level_quiz_get_top]";
            this.pr_level_quiz_get_top.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_level_quiz_get_top.Connection = this.sqlConnection;
            this.pr_level_quiz_get_top.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_film_upd_status
            // 
            this.pr_film_upd_status.CommandText = "[pr_film_upd_status]";
            this.pr_film_upd_status.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_film_upd_status.Connection = this.sqlConnection;
            this.pr_film_upd_status.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@status", System.Data.SqlDbType.Bit, 1)});
            // 
            // pr_course_details_get
            // 
            this.pr_course_details_get.CommandText = "[pr_course_details_get]";
            this.pr_course_details_get.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_course_details_get.Connection = this.sqlConnection;
            this.pr_course_details_get.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@course_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_course_update
            // 
            this.pr_course_update.CommandText = "[pr_course_update]";
            this.pr_course_update.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_course_update.Connection = this.sqlConnection;
            this.pr_course_update.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@course_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@number", System.Data.SqlDbType.UniqueIdentifier, 16),
            new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.NVarChar, 100),
            new System.Data.SqlClient.SqlParameter("@description", System.Data.SqlDbType.NVarChar, 300),
            new System.Data.SqlClient.SqlParameter("@detailed_description", System.Data.SqlDbType.NText, 1073741823),
            new System.Data.SqlClient.SqlParameter("@picture_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@currency_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@price", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, false, ((byte)(18)), ((byte)(2)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@ui_langauge_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@location_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@category_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@url", System.Data.SqlDbType.VarChar, 100),
            new System.Data.SqlClient.SqlParameter("@authors", System.Data.SqlDbType.NVarChar, 1000),
            new System.Data.SqlClient.SqlParameter("@contacts", System.Data.SqlDbType.NVarChar, 1000),
            new System.Data.SqlClient.SqlParameter("@tags", System.Data.SqlDbType.NVarChar, 1000),
            new System.Data.SqlClient.SqlParameter("@links", System.Data.SqlDbType.NVarChar, 4000),
            new System.Data.SqlClient.SqlParameter("@is_editable", System.Data.SqlDbType.Bit, 1),
            new System.Data.SqlClient.SqlParameter("@is_copied", System.Data.SqlDbType.Bit, 1),
            new System.Data.SqlClient.SqlParameter("@is_public", System.Data.SqlDbType.Bit, 1),
            new System.Data.SqlClient.SqlParameter("@password", System.Data.SqlDbType.NVarChar, 100),
            new System.Data.SqlClient.SqlParameter("@google_advertisement", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@is_approved", System.Data.SqlDbType.Bit, 1),
            new System.Data.SqlClient.SqlParameter("@result_id", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.InputOutput, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_modules_for_course_lst
            // 
            this.pr_modules_for_course_lst.CommandText = "[pr_modules_for_course_lst]";
            this.pr_modules_for_course_lst.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_modules_for_course_lst.Connection = this.sqlConnection;
            this.pr_modules_for_course_lst.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@course_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_module_get
            // 
            this.pr_module_get.CommandText = "[pr_module_get]";
            this.pr_module_get.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_module_get.Connection = this.sqlConnection;
            this.pr_module_get.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@module_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_rating_update
            // 
            this.pr_rating_update.CommandText = "[pr_rating_update]";
            this.pr_rating_update.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_rating_update.Connection = this.sqlConnection;
            this.pr_rating_update.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@target_element", System.Data.SqlDbType.UniqueIdentifier, 16),
            new System.Data.SqlClient.SqlParameter("@value", System.Data.SqlDbType.Float, 8)});
            // 
            // pr_course_get_list
            // 
            this.pr_course_get_list.CommandText = "[pr_course_get_list]";
            this.pr_course_get_list.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_course_get_list.Connection = this.sqlConnection;
            this.pr_course_get_list.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@is_public", System.Data.SqlDbType.Bit, 1)});
            // 
            // pr_picture_get
            // 
            this.pr_picture_get.CommandText = "[pr_picture_get]";
            this.pr_picture_get.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_picture_get.Connection = this.sqlConnection;
            this.pr_picture_get.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@picture_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_user_comment_add
            // 
            this.pr_user_comment_add.CommandText = "[pr_user_comment_add]";
            this.pr_user_comment_add.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_user_comment_add.Connection = this.sqlConnection;
            this.pr_user_comment_add.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@target_element", System.Data.SqlDbType.UniqueIdentifier, 16),
            new System.Data.SqlClient.SqlParameter("@comment_text", System.Data.SqlDbType.NVarChar, 500),
            new System.Data.SqlClient.SqlParameter("@created_date", System.Data.SqlDbType.DateTime, 8),
            new System.Data.SqlClient.SqlParameter("@parent_comment_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@result_id", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.InputOutput, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_user_comment_lst
            // 
            this.pr_user_comment_lst.CommandText = "[pr_user_comment_lst]";
            this.pr_user_comment_lst.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_user_comment_lst.Connection = this.sqlConnection;
            this.pr_user_comment_lst.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@target_element", System.Data.SqlDbType.UniqueIdentifier, 16),
            new System.Data.SqlClient.SqlParameter("@page_size", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@page_number", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_exercise_text_get
            // 
            this.pr_exercise_text_get.CommandText = "[pr_exercise_text_get]";
            this.pr_exercise_text_get.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_text_get.Connection = this.sqlConnection;
            this.pr_exercise_text_get.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@exercise_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_exercise_text_list
            // 
            this.pr_exercise_text_list.CommandText = "[pr_exercise_text_list]";
            this.pr_exercise_text_list.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_text_list.Connection = this.sqlConnection;
            this.pr_exercise_text_list.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@module_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_exercise_text_ins
            // 
            this.pr_exercise_text_ins.CommandText = "[pr_exercise_text_ins]";
            this.pr_exercise_text_ins.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_text_ins.Connection = this.sqlConnection;
            this.pr_exercise_text_ins.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.NVarChar, 200),
            new System.Data.SqlClient.SqlParameter("@description", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@module_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@text", System.Data.SqlDbType.NVarChar, 2147483647)});
            // 
            // pr_exercise_text_upd
            // 
            this.pr_exercise_text_upd.CommandText = "[pr_exercise_text_upd]";
            this.pr_exercise_text_upd.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_text_upd.Connection = this.sqlConnection;
            this.pr_exercise_text_upd.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@exercise_text_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.NVarChar, 200),
            new System.Data.SqlClient.SqlParameter("@description", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@module_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@text", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@ordinal_number", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@result_id", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.InputOutput, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_exercise_text_del
            // 
            this.pr_exercise_text_del.CommandText = "[pr_exercise_text_del]";
            this.pr_exercise_text_del.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_text_del.Connection = this.sqlConnection;
            this.pr_exercise_text_del.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@exercise_text_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_payment_for_course_add
            // 
            this.pr_payment_for_course_add.CommandText = "[pr_payment_for_course_add]";
            this.pr_payment_for_course_add.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_payment_for_course_add.Connection = this.sqlConnection;
            this.pr_payment_for_course_add.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@course_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@payment_date", System.Data.SqlDbType.DateTime, 8),
            new System.Data.SqlClient.SqlParameter("@currency_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@payment_value", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, false, ((byte)(18)), ((byte)(2)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@result_id", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.InputOutput, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_exercise_select_get
            // 
            this.pr_exercise_select_get.CommandText = "[pr_exercise_select_get]";
            this.pr_exercise_select_get.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_select_get.Connection = this.sqlConnection;
            this.pr_exercise_select_get.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@exercise_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_exercise_select_answer_list
            // 
            this.pr_exercise_select_answer_list.CommandText = "[pr_exercise_select_answer_list]";
            this.pr_exercise_select_answer_list.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_select_answer_list.Connection = this.sqlConnection;
            this.pr_exercise_select_answer_list.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@exercise_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_exercise_select_ins
            // 
            this.pr_exercise_select_ins.CommandText = "[pr_exercise_select_ins]";
            this.pr_exercise_select_ins.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_select_ins.Connection = this.sqlConnection;
            this.pr_exercise_select_ins.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.NVarChar, 200),
            new System.Data.SqlClient.SqlParameter("@description", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@module_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@text", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@picture_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_exercise_select_upd
            // 
            this.pr_exercise_select_upd.CommandText = "[pr_exercise_select_upd]";
            this.pr_exercise_select_upd.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_select_upd.Connection = this.sqlConnection;
            this.pr_exercise_select_upd.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@exercise_select_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.NVarChar, 200),
            new System.Data.SqlClient.SqlParameter("@description", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@module_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@text", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@picture_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@ordinal_number", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@result_id", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.InputOutput, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_exercise_select_del
            // 
            this.pr_exercise_select_del.CommandText = "[pr_exercise_select_del]";
            this.pr_exercise_select_del.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_select_del.Connection = this.sqlConnection;
            this.pr_exercise_select_del.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@exercise_select_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_answer_ins
            // 
            this.pr_answer_ins.CommandText = "[pr_answer_ins]";
            this.pr_answer_ins.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_answer_ins.Connection = this.sqlConnection;
            this.pr_answer_ins.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@exercise_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@text", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@picture_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@is_right", System.Data.SqlDbType.Bit, 1)});
            // 
            // pr_answer_upd
            // 
            this.pr_answer_upd.CommandText = "[pr_answer_upd]";
            this.pr_answer_upd.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_answer_upd.Connection = this.sqlConnection;
            this.pr_answer_upd.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@answer_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@exercise_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@text", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@picture_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@is_right", System.Data.SqlDbType.Bit, 1),
            new System.Data.SqlClient.SqlParameter("@result_id", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.InputOutput, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_answer_del
            // 
            this.pr_answer_del.CommandText = "[pr_answer_del]";
            this.pr_answer_del.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_answer_del.Connection = this.sqlConnection;
            this.pr_answer_del.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@answer_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_payment_for_module_add
            // 
            this.pr_payment_for_module_add.CommandText = "[pr_payment_for_module_add]";
            this.pr_payment_for_module_add.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_payment_for_module_add.Connection = this.sqlConnection;
            this.pr_payment_for_module_add.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@module_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@payment_date", System.Data.SqlDbType.DateTime, 8),
            new System.Data.SqlClient.SqlParameter("@currency_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@payment_value", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, false, ((byte)(18)), ((byte)(2)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@discount_rate", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, false, ((byte)(18)), ((byte)(8)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@result_id", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.InputOutput, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_account_money_balance_lst
            // 
            this.pr_account_money_balance_lst.CommandText = "[pr_account_money_balance_lst]";
            this.pr_account_money_balance_lst.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_account_money_balance_lst.Connection = this.sqlConnection;
            this.pr_account_money_balance_lst.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_picture_update
            // 
            this.pr_picture_update.CommandText = "[pr_picture_update]";
            this.pr_picture_update.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_picture_update.Connection = this.sqlConnection;
            this.pr_picture_update.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@picture_data", System.Data.SqlDbType.VarBinary, 2147483647),
            new System.Data.SqlClient.SqlParameter("@result_id", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.InputOutput, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_currency_lst
            // 
            this.pr_currency_lst.CommandText = "[pr_currency_lst]";
            this.pr_currency_lst.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_currency_lst.Connection = this.sqlConnection;
            this.pr_currency_lst.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_exercises_for_module_lst
            // 
            this.pr_exercises_for_module_lst.CommandText = "[pr_exercises_for_module_lst]";
            this.pr_exercises_for_module_lst.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercises_for_module_lst.Connection = this.sqlConnection;
            this.pr_exercises_for_module_lst.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@module_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_module_update
            // 
            this.pr_module_update.CommandText = "[pr_module_update]";
            this.pr_module_update.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_module_update.Connection = this.sqlConnection;
            this.pr_module_update.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@module_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@course_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.VarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@description", System.Data.SqlDbType.VarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@detailed_description", System.Data.SqlDbType.VarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@picture_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@currency_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@price", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, false, ((byte)(18)), ((byte)(2)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@url", System.Data.SqlDbType.VarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@tags", System.Data.SqlDbType.VarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@links", System.Data.SqlDbType.VarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@exercise_max_number", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@order_in_course", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_exercise_text_answer_get
            // 
            this.pr_exercise_text_answer_get.CommandText = "[pr_exercise_text_answer_get]";
            this.pr_exercise_text_answer_get.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_text_answer_get.Connection = this.sqlConnection;
            this.pr_exercise_text_answer_get.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@exercise_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_exercise_text_answer_ins
            // 
            this.pr_exercise_text_answer_ins.CommandText = "[pr_exercise_text_answer_ins]";
            this.pr_exercise_text_answer_ins.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_text_answer_ins.Connection = this.sqlConnection;
            this.pr_exercise_text_answer_ins.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@exercise_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@text", System.Data.SqlDbType.NVarChar, 2147483647)});
            // 
            // pr_exercise_text_answer_upd
            // 
            this.pr_exercise_text_answer_upd.CommandText = "[pr_exercise_text_answer_upd]";
            this.pr_exercise_text_answer_upd.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_text_answer_upd.Connection = this.sqlConnection;
            this.pr_exercise_text_answer_upd.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@text", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@mark", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@result_id", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.InputOutput, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_exercise_answer_text_get
            // 
            this.pr_exercise_answer_text_get.CommandText = "[pr_exercise_answer_text_get]";
            this.pr_exercise_answer_text_get.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_answer_text_get.Connection = this.sqlConnection;
            this.pr_exercise_answer_text_get.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@exercise_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_exercise_answer_text_ins
            // 
            this.pr_exercise_answer_text_ins.CommandText = "[pr_exercise_answer_text_ins]";
            this.pr_exercise_answer_text_ins.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_answer_text_ins.Connection = this.sqlConnection;
            this.pr_exercise_answer_text_ins.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.NVarChar, 200),
            new System.Data.SqlClient.SqlParameter("@description", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@module_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@text", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_exercise_answer_text_upd
            // 
            this.pr_exercise_answer_text_upd.CommandText = "[pr_exercise_answer_text_upd]";
            this.pr_exercise_answer_text_upd.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_answer_text_upd.Connection = this.sqlConnection;
            this.pr_exercise_answer_text_upd.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@exercise_text_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.NVarChar, 200),
            new System.Data.SqlClient.SqlParameter("@description", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@module_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@text", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@ordinal_number", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@result_id", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.InputOutput, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_exercise_select_text_get
            // 
            this.pr_exercise_select_text_get.CommandText = "[pr_exercise_select_text_get]";
            this.pr_exercise_select_text_get.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_select_text_get.Connection = this.sqlConnection;
            this.pr_exercise_select_text_get.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@exercise_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_exercise_select_text_ins
            // 
            this.pr_exercise_select_text_ins.CommandText = "[pr_exercise_select_text_ins]";
            this.pr_exercise_select_text_ins.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_select_text_ins.Connection = this.sqlConnection;
            this.pr_exercise_select_text_ins.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.NVarChar, 200),
            new System.Data.SqlClient.SqlParameter("@description", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@module_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@text", System.Data.SqlDbType.NVarChar, 2147483647)});
            // 
            // pr_exercise_select_text_upd
            // 
            this.pr_exercise_select_text_upd.CommandText = "[pr_exercise_select_text_upd]";
            this.pr_exercise_select_text_upd.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_select_text_upd.Connection = this.sqlConnection;
            this.pr_exercise_select_text_upd.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@exercise_select_text_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.NVarChar, 200),
            new System.Data.SqlClient.SqlParameter("@description", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@module_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@text", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@ordinal_number", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@result_id", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.InputOutput, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_exercise_skip_text_get
            // 
            this.pr_exercise_skip_text_get.CommandText = "[pr_exercise_skip_text_get]";
            this.pr_exercise_skip_text_get.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_skip_text_get.Connection = this.sqlConnection;
            this.pr_exercise_skip_text_get.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@exercise_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_exercise_skip_text_ins
            // 
            this.pr_exercise_skip_text_ins.CommandText = "[pr_exercise_skip_text_ins]";
            this.pr_exercise_skip_text_ins.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_skip_text_ins.Connection = this.sqlConnection;
            this.pr_exercise_skip_text_ins.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.NVarChar, 200),
            new System.Data.SqlClient.SqlParameter("@description", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@module_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@text", System.Data.SqlDbType.NVarChar, 2147483647)});
            // 
            // pr_exercise_skip_text_upd
            // 
            this.pr_exercise_skip_text_upd.CommandText = "[pr_exercise_skip_text_upd]";
            this.pr_exercise_skip_text_upd.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercise_skip_text_upd.Connection = this.sqlConnection;
            this.pr_exercise_skip_text_upd.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@exercise_skip_text_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.NVarChar, 200),
            new System.Data.SqlClient.SqlParameter("@description", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@module_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@text", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@ordinal_number", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@result_id", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.InputOutput, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null)});
            // 
            // pr_tv_get_other_list
            // 
            this.pr_tv_get_other_list.CommandText = "[pr_tv_get_other_list]";
            this.pr_tv_get_other_list.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_tv_get_other_list.Connection = this.sqlConnection;
            this.pr_tv_get_other_list.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_tv_update_or_insert
            // 
            this.pr_tv_update_or_insert.CommandText = "[pr_tv_update_or_insert]";
            this.pr_tv_update_or_insert.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_tv_update_or_insert.Connection = this.sqlConnection;
            this.pr_tv_update_or_insert.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@url", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@image_url", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@description", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@is_editable", System.Data.SqlDbType.Bit, 1)});
            // 
            // pr_course_approve
            // 
            this.pr_course_approve.CommandText = "[pr_course_approve]";
            this.pr_course_approve.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_course_approve.Connection = this.sqlConnection;
            this.pr_course_approve.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@course_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_tv_delete
            // 
            this.pr_tv_delete.CommandText = "[pr_tv_delete]";
            this.pr_tv_delete.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_tv_delete.Connection = this.sqlConnection;
            this.pr_tv_delete.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@tv_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_tv_update_order
            // 
            this.pr_tv_update_order.CommandText = "[pr_tv_update_order]";
            this.pr_tv_update_order.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_tv_update_order.Connection = this.sqlConnection;
            this.pr_tv_update_order.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@ordering_string", System.Data.SqlDbType.VarChar, 2147483647)});
            // 
            // pr_modules_for_course_order
            // 
            this.pr_modules_for_course_order.CommandText = "[pr_modules_for_course_order]";
            this.pr_modules_for_course_order.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_modules_for_course_order.Connection = this.sqlConnection;
            this.pr_modules_for_course_order.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@ordering_string", System.Data.SqlDbType.VarChar, 2147483647)});
            // 
            // pr_exercises_for_module_order
            // 
            this.pr_exercises_for_module_order.CommandText = "[pr_exercises_for_module_order]";
            this.pr_exercises_for_module_order.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_exercises_for_module_order.Connection = this.sqlConnection;
            this.pr_exercises_for_module_order.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@ordering_string", System.Data.SqlDbType.VarChar, 2147483647)});
            // 
            // pr_module_delete
            // 
            this.pr_module_delete.CommandText = "[pr_module_delete]";
            this.pr_module_delete.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_module_delete.Connection = this.sqlConnection;
            this.pr_module_delete.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@module_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_fm_delete
            // 
            this.pr_fm_delete.CommandText = "[pr_fm_delete]";
            this.pr_fm_delete.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_fm_delete.Connection = this.sqlConnection;
            this.pr_fm_delete.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@fm_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_fm_get_other_list
            // 
            this.pr_fm_get_other_list.CommandText = "[pr_fm_get_other_list]";
            this.pr_fm_get_other_list.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_fm_get_other_list.Connection = this.sqlConnection;
            this.pr_fm_get_other_list.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_fm_update_or_insert
            // 
            this.pr_fm_update_or_insert.CommandText = "[pr_fm_update_or_insert]";
            this.pr_fm_update_or_insert.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_fm_update_or_insert.Connection = this.sqlConnection;
            this.pr_fm_update_or_insert.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@url", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@image_url", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@description", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@is_editable", System.Data.SqlDbType.Bit, 1),
            new System.Data.SqlClient.SqlParameter("@use_media_player", System.Data.SqlDbType.Bit, 1)});
            // 
            // pr_fm_update_order
            // 
            this.pr_fm_update_order.CommandText = "[pr_fm_update_order]";
            this.pr_fm_update_order.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_fm_update_order.Connection = this.sqlConnection;
            this.pr_fm_update_order.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@ordering_string", System.Data.SqlDbType.VarChar, 2147483647)});
            // 
            // pr_user_course_password_check_and_update
            // 
            this.pr_user_course_password_check_and_update.CommandText = "[pr_user_course_password_check_and_update]";
            this.pr_user_course_password_check_and_update.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_user_course_password_check_and_update.Connection = this.sqlConnection;
            this.pr_user_course_password_check_and_update.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@user_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@course_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@password", System.Data.SqlDbType.NVarChar, 100)});
            // 
            // pr_user_comment_items_count
            // 
            this.pr_user_comment_items_count.CommandText = "[pr_user_comment_items_count]";
            this.pr_user_comment_items_count.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_user_comment_items_count.Connection = this.sqlConnection;
            this.pr_user_comment_items_count.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@target_element", System.Data.SqlDbType.UniqueIdentifier, 16)});
            // 
            // pr_user_comment_claim
            // 
            this.pr_user_comment_claim.CommandText = "[pr_user_comment_claim]";
            this.pr_user_comment_claim.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_user_comment_claim.Connection = this.sqlConnection;
            this.pr_user_comment_claim.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@user_comment_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_user_comment_rate
            // 
            this.pr_user_comment_rate.CommandText = "[pr_user_comment_rate]";
            this.pr_user_comment_rate.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_user_comment_rate.Connection = this.sqlConnection;
            this.pr_user_comment_rate.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@user_comment_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@is_positive", System.Data.SqlDbType.Bit, 1)});
            // 
            // pr_film_get_list
            // 
            this.pr_film_get_list.CommandText = "[pr_film_get_list]";
            this.pr_film_get_list.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_film_get_list.Connection = this.sqlConnection;
            this.pr_film_get_list.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_film_get_other_list
            // 
            this.pr_film_get_other_list.CommandText = "[pr_film_get_other_list]";
            this.pr_film_get_other_list.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_film_get_other_list.Connection = this.sqlConnection;
            this.pr_film_get_other_list.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@learn_language_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_film_update_or_insert
            // 
            this.pr_film_update_or_insert.CommandText = "[pr_film_update_or_insert]";
            this.pr_film_update_or_insert.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_film_update_or_insert.Connection = this.sqlConnection;
            this.pr_film_update_or_insert.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@native_language_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@player_code", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@image_url", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@description", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@is_editable", System.Data.SqlDbType.Bit, 1),
            new System.Data.SqlClient.SqlParameter("@category_id", System.Data.SqlDbType.Int, 4)});
            // 
            // pr_film_update_order
            // 
            this.pr_film_update_order.CommandText = "[pr_film_update_order]";
            this.pr_film_update_order.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_film_update_order.Connection = this.sqlConnection;
            this.pr_film_update_order.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@ordering_string", System.Data.SqlDbType.VarChar, 2147483647)});
            // 
            // pr_film_delete
            // 
            this.pr_film_delete.CommandText = "[pr_film_delete]";
            this.pr_film_delete.CommandType = System.Data.CommandType.StoredProcedure;
            this.pr_film_delete.Connection = this.sqlConnection;
            this.pr_film_delete.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@account_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@film_id", System.Data.SqlDbType.Int, 4)});

		}

        #endregion
    }
}