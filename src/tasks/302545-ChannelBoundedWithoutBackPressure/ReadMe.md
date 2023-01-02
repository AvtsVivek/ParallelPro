

- Even thoought we are trying to put an element every one second, and that an element is being pulled out every two seconds, there is no back pressure being built up. 
- Back pressure builds up, if there are more and more items being put into the system than the nubmer of itmes being retrived.

- Note that the This is bounded channel with only one elment(see the ctor). 

- Even though we are trying to put an element every one second, the channel is bounded at 1 element. So it has to wait till its empty again. And this happens every two seconds.

- Since it is limited to 1 element, there is no back pressure.


This is taken from the following.

[Working with Channels in .NET](https://www.youtube.com/watch?v=gT06qvQLtJ0)



