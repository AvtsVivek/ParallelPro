
#Declarative Synchronization

- This is an alternative is to earlier versions of synchronization. Here all of the fields and methods in a class are involved in the sync process. This is done by applying the Synchronization attribute. The class must extend System.ContextBoundObject and import the System.Runtime.Remoting.Contexts namespace in order to be able to use the Synchronization attribute. 

- To demonstrate declarative synchronization, let the BankAccount class have methods so that the balance can be read with the GetBalance() method and incremented with the IncrementBalance() method. 

- Now, all of the code statements are contained in a single class and can be synchronized by applying the Synchronization attribute and having the
BankAccount class extend ContextBoundObject.

- The downside with using the Synchronization attribute is that every field and method of your class, even if they don’t modify shared data, becomes synchronized using a single lock, and this can cause a performance problem. 

- Declarative synchronization is a heavy-handed approach to avoiding data races and should be used with caution.

