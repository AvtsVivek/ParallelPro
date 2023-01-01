
# Unexpected Mutability

- Types that are assumed to be immutable are built from mutable types whose states are changed by another Task. 
- Unexpected and inconsistent program results from the point at which the state change occurs.

- There is no programmatic solution to the Unexpected Mutability antipattern. 
- The only way to avoid this problem is to check the field modifiers for all types that you are relying on as being immutable to make sure that they cannot be changed.

- C# does not enforce immutability of complex types; it is possible to declare a field to be readonly and still modify the members of the type instance assigned to it. 

- For example, the following listing shows the type MyImmutableType, which declares a readonly field of the type MyReferenceData. 

- The PI field of MyReferenceData is not readonly and is changed by the main thread of the program, causing incorrect calculations.

- 