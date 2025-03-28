namespace ProjectsMecsaSPA.Model.Bitrix
{
    public class BitrixFileResponse
    {
        public Result result { get; set; }
    }

    public class Result
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public string STORAGE_ID { get; set; }
        public string TYPE { get; set; }
        public string PARENT_ID { get; set; }
        public int DELETED_TYPE { get; set; }
        public int GLOBAL_CONTENT_VERSION { get; set; }
        public int FILE_ID { get; set; }
        public string SIZE { get; set; }
        public string CREATE_TIME { get; set; }
        public string UPDATE_TIME { get; set; }
        public string DELETE_TIME { get; set; }
        public string CREATED_BY { get; set; }
        public string UPDATED_BY { get; set; }
        public string DELETED_BY { get; set; }
        public string DOWNLOAD_URL { get; set; }
        public string DETAIL_URL { get; set; }
    }
}


