using System;

using R5T.Magyar;


namespace Amazon
{
    public static class AwsHelper
    {
        /// <summary>
        /// If you want more than the <see cref="DefaultMaximumResultsPerPageCount"/> results per page, you can request a greater number of results per page.
        /// However, you can only request a greater number of results up to this allowed maximum results per page.
        /// You might want to request ALL results in a large list of results, but this allowed maximum means that you must instead get those results a batch at a time, which requires a fixed network latency per batch.
        /// This encourages use of query parameters to limit the size of a request result set.
        /// </summary>
        public const int MaximumAllowedMaximumResultsPerPageCount = 1000;
        /// <summary>
        /// By default, results will be returned in batches of this many items.
        /// If a request returns more items than the per-page key count, paging of multiple pages of results will be required.
        /// </summary>
        public const int DefaultMaximumResultsPerPageCount = 100;


        public static bool IsGreaterThanMaximumAllowedMaximumResultsPerPageCount(int requestedMaximumResultsPerPageCount)
        {
            var output = requestedMaximumResultsPerPageCount > AwsHelper.MaximumAllowedMaximumResultsPerPageCount;
            return output;
        }

        /// <summary>
        /// Get the actual number of results per page that will be returned by an AWS request.
        /// You might specify an unlimited number of results per page, but the AWS infrastructure will limit you.
        /// </summary>
        /// <returns>The <see cref="MaximumAllowedMaximumResultsPerPageCount"/> if the <paramref name="requestedMaximumResultsPerPageCount"/> is greater than the maximum allowed maximum results per page, otherwise the <paramref name="requestedMaximumResultsPerPageCount"/>.</returns>
        public static int GetAwsActualMaximumResultsPerPageCount(int requestedMaximumResultsPerPageCount)
        {
            if (AwsHelper.IsGreaterThanMaximumAllowedMaximumResultsPerPageCount(requestedMaximumResultsPerPageCount)
                || QueryHelper.IsNoLimitMaximumResultsCount(requestedMaximumResultsPerPageCount))
            {
                // If you request more than the allowed maximum, or no limit, you will actually be limited by the AWS infrastructure.
                return AwsHelper.MaximumAllowedMaximumResultsPerPageCount;
            }
            else
            {
                // Otherwise, your requested value is fine.
                return requestedMaximumResultsPerPageCount;
            }
        }
    }
}
