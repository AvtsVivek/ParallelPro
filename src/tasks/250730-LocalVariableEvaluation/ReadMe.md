

- Local Variable Evaluation

- Assume that you create a series of Tasks in a for loop and refer to the loop counter in your lambda
expressions. All of the Tasks end up with the same value because of the way that the C# variable scoping
rules are applied to lambda expressions.

- Solution

- The simplest way to fix this problem is to pass the loop counter in as a state object to the Task.

- Example

- In the following example, five Tasks print out a message that references the counter of the loop that
created them, and they all print the same value. Another five Tasks do the same thing, but get their
values as state objects, and these get the expected values.



