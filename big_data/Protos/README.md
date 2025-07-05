https://protobuf.dev/programming-guides/proto3/

# Scalar Types (primitives)

Include all numbers, string, bool, enum, ... everything other than `message (object)`
always return default value
string "" (empty string)
int32 0
double 0.0
bool false
enum First value (e.g., UNSPECIFIED = 0)

`optional` keyword does two things in the generated class:

- adds .HasName property (getter) bool
- adds .ClearName() method to mark as unset. (actual implementation sets the real name\_ backing var to null for string, and for numbers sets some tracking var to "not set")

string in C# utilizes null under the hood, but can't set to null via setter, setter throws if pass null. Uses fake default value hardcoded "" empty string.

always return default value via getter, but you check via .HasName to see if set or unset (same semantic meaning as null)

# Message Types (objects)

Company, FieldMask, all Google wrappers (but not used anymore)
Can be assigned null.
if null is assigned then means not set.
Don't have .HasCompany methods. Just use `if (request.Company != null)`
`optional` keyword does nothing. Do not use.

# enums

enums are scalar (primitive)
must have `unspecified = 0` and it is the default value, like "" for strings, false for bool.

Both `0 unspecified` and `null` in DB would mean the same thing.
My idea. Do not use null in DB. just use 0 for "nothing". Maybe even non null column in DB.

# date / time

Google-wrapper that is still relevant in proto V3. There's no alternative, no native date/time type. Only this one from Google.
google.protobuf.Timestamp date = 6;

no "optional" because it's a Google wrapper type. It can be null. so if (grpc.date != null)

# Field mask for PATCH

google.protobuf.FieldMask
