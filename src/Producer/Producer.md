# Pulsar Producer

The Producer is a simple console application that writes a message to Pulsar topic. It accepts 2 positional parameters:

- The Pulsar host (as `<HOST>:<PORT>` **_WITHOUT_** `pulsar://`)
- The target topic

The message can either be passed in as a third positional parameter, or can be passed in via `stdin`.

For example,

`./Producer localhost:6650 persistent://public/default/msg-topic "Hello, World!"`
 
or

`echo "Hello, World!" | ./Producer localhost:6650 persistent://public/default/msg-topic`