# Issues Found :triangular_flag_on_post:
This document registers all issues found during this library implementation at Wootrade API.


| Type          | Description                                                                                                                                               | Status |
|---------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------|--------|
| Bug           | Cancel Order endpoint (DELETE /v1/order) not working. The response is  {  "success": false,  "code": -1000,  "message": "unknown error"}                  | :x:    |
| Bug           | Cancel Order by Client Order Id (DELETE /v1/client/order) not working. The response is  {  "success": false,  "code": -1000,  "message": "unknown error"} | :x:    |
| Bug           | Cancel Orders by Symbol (DELETE /v1/orders) not working. The response is {  "success": false,  "code": -1000,  "message": "unknown error"}             | :x:    |
| Suggestion    | Place Order returns code -1012 instead of -1007 (DUPLICATE_REQUEST). The response is {  "success": false,  "code": -1012,  "message": "duplicated client_order_id"}        | :x:    |
| Suggestion    | Documentation does not refer when a timestamp is represented in Milliseconds or Seconds. Should Wootrade define a standard for DateTime representation? | :x:    |
| Documentation | Wrong documentation. Merge this pull request to fix: https://github.com/kronosresearch/wootrade-documents/pull/1                                          | :x:    |
| Documentation | "Send Order" endpoint 'order_type' parameter is missing other Order types available.                                                                      | :x:    |


:x: -> Not solved

:heavy_check_mark: -> Fixed
