Made raiser wait all callbacks processed.
Seem cannot make both calls (direct and back) blocking – WCF hangs.

*Used solution:
Made host raise additional callback that everything was processed.
Client contains dictionary of raised events and wait handles.
Waits until everything proceeds.

*Possible solution:
Seems it is possible to fix problem using several of next things:
1. Service preserves dictionaries with session ids, message ids and wait handles
2. Each client calls Proceed on service after it done it callback
3. Creating new threads manually
4. Do not call caller callback