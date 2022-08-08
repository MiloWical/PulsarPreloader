# Bash Scripts

The scripts contained here provide a wrapper around calling the [Producer](../Producer/Producer.md) and submitting payloads.

## `generate-payload.sh`

This file creates a payload that can be written to Pulsar. Currently, the payload looks like this:

```json
{
  "id": "<UUID"
}
```

This output is passed to `stdout` for further processing.

## `write-messages.sh`

This file accepts accepts 3 positional parameters:

- The number of messages to send
- The host address of the Pulsar server
- The topic name where the messages are to be sent.

It calls the `generate_payload()` function in [`generate-payload.sh`](./generate-payload.sh) to generate a payload then writes it to the specified topic on the specified host for the specified number of iterations.