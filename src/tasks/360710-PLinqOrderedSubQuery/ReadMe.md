

You should only use the AsOrdered() method if the order of the results in relation to the source data
is important (i.e., you need the nth result item to be the result of processing the nth source item). In such
situations, creating a dynamic type that preserves this relationship is often more efficient. This ex
shows the common misuse of AsOrdered() and a more efficient alternative
