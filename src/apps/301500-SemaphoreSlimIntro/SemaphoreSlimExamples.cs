using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemaphoreSlimIntro
{
    public class SemaphoreSlimExamples
    {
        private readonly SemaphoreSlim _semaphoreSlim;
        public SemaphoreSlimExamples(int initialCount) 
        {
            // semaphoreSlim is like a box of keys. Keys are for a room like conf or bath room. 
            // If you want to use the room, then you need a key.
            // If there is no key in the box, you have to wait for the key to go use the room. 
            // So here we creating a SemaphoreSlim, we are creating a box with initialCount of keys in it.
            _semaphoreSlim = new SemaphoreSlim(initialCount);
        }
        public async Task ZeroCountExample()
        {
            // So here there are zero keys, so cannot do work
            Console.WriteLine("Start");
            Console.WriteLine($"The current count before calling wait async is {_semaphoreSlim.CurrentCount}");
            await _semaphoreSlim.WaitAsync();
            Console.WriteLine($"The current count after calling wait async is {_semaphoreSlim.CurrentCount}");
            Console.WriteLine("Do some work");
            Console.WriteLine("End");
        }

        public async Task OneCountExample()
        {
            // So here there is one keys, so CAN do work.
            Console.WriteLine("Start");
            Console.WriteLine($"The current count before calling wait async is {_semaphoreSlim.CurrentCount}");
            await _semaphoreSlim.WaitAsync();
            Console.WriteLine($"The current count after calling wait async is {_semaphoreSlim.CurrentCount}");
            Console.WriteLine("Do some work");
            Console.WriteLine("End");
        }

        public async Task OneKeyWithTenPeople()
        {
            // So here there is one keys but 10 ppl, so only one CAN do work.
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Start");
                Console.WriteLine($"The current count before calling wait async is {_semaphoreSlim.CurrentCount}");
                await _semaphoreSlim.WaitAsync();
                Console.WriteLine($"The current count after calling wait async is {_semaphoreSlim.CurrentCount}");
                Console.WriteLine("Do some work");
                Console.WriteLine("End");
            }
        }

        public async Task OneKeyWithTenPeopleWithRelease()
        {
            // So here there is one keys but 10 ppl. But each one will put back the key. So each person can access the room one after the other.
            // We are putting back the key using Release.
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Start");
                Console.WriteLine($"The current count before calling wait async is {_semaphoreSlim.CurrentCount}");
                await _semaphoreSlim.WaitAsync();
                Console.WriteLine($"The current count after calling wait async is {_semaphoreSlim.CurrentCount}");
                Console.WriteLine($"Do some work by Person {i}");
                _semaphoreSlim.Release();
                Console.WriteLine("End");
            }
        }
        public async Task TenKeysWithTenPeople()
        {
            // So here there are 10 keys and 10 ppl. So all can access the room.
            
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Start");
                Console.WriteLine($"The current count before calling wait async is {_semaphoreSlim.CurrentCount}");
                await _semaphoreSlim.WaitAsync();
                Console.WriteLine($"The current count after calling wait async is {_semaphoreSlim.CurrentCount}");
                Console.WriteLine($"Do some work by Person {i}");
                Console.WriteLine("End");
            }
        }
    }
}