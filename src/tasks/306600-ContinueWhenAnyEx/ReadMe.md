# Continuation Tasks

- The ContinueWhenAny() method works in a very similar way to ContinueWhenAll(), except that the continuation Task will be scheduled to run as soon as any of the antecedent Tasks has completed. 
- The delegate argument for the ContinueWhenAny() method is the antecedent Task that completed first. 
- This demo demonstrates this kind of continuation. 
- A set of ten antecedent Tasks uses a random number generator to wait for a period of time, and the first Task to wake up finishes and becomes the antecedent to the continuation Task.

- 