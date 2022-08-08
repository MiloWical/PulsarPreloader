# Pulsar Consumer

The Consumer is a simple console application that reads messages from a Pulsar topic. It has 3 mandatory positional parameters:

- The Pulsar host (as `<HOST>:<PORT>` **_WITHOUT_** `pulsar://`)
- The target topic
- The subscription name

Additionally, there is a fourth optional parameter that specifies a number of messages to read; if absent, it will listen indefinitely.

For example,

`./Consumer localhost:6650 persistent://public/default/msg-topic Consumer 10` will read 10 messages and then quit (blocking until all 10 are read), whereas

`./Consumer localhost:6650 persistent://public/default/msg-topic Consumer` will read indefinitely as messages are received.