# Issues Found :triangular_flag_on_post:
This document registers all issues found during this library implementation at Wootrade API.


| # | Type          | Description                                                                                                                                               | Status |
|----|---------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------|--------|
| 1 | Suggestion    | Place Order returns code -1012 instead of -1007 (DUPLICATE_REQUEST). The response is {  "success": false,  "code": -1012,  "message": "duplicated client_order_id"}        | :heavy_check_mark:    |
| 2 | Suggestion    | Documentation does not refer when a timestamp/creation date/execution date/updated date is represented in Milliseconds or Seconds. Should Wootrade define a standard for DateTime representation? | :heavy_check_mark:    |
| 3 | Documentation | Wrong documentation. Merge this pull request to fix: https://github.com/kronosresearch/wootrade-documents/pull/1                                          | :heavy_check_mark:    |
| 4 | Documentation | "Send Order" endpoint 'order_type' parameter is missing other Order types available.                                                                      | :heavy_check_mark:    |


:x: -> Not solved

:heavy_check_mark: -> Fixed
