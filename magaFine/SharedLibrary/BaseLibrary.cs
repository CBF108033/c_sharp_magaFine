using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace magaFine.SharedLibrary
{
    public class BaseLibrary
    {
        protected OperationResult _operationResult { get; private set; } = new OperationResult();

        /**
         * 設定或取得 OperationResult 屬性
         */
        public OperationResult StatusMessage(bool? success = null, string msg = null)
        {
            // 如果提供了參數，就更新 OperationResult 的屬性
            if (success.HasValue)
            {
                _operationResult.Success = success.Value;
            }
            if (!string.IsNullOrEmpty(msg))
            {
                _operationResult.Message = msg;
            }

            return _operationResult;
        }
    }

    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}