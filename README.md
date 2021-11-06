# Unity Quad Tree Example
![image](https://user-images.githubusercontent.com/90412342/140616446-a00a4126-7ddb-4831-9216-76b4b5033312.png)

Who doesn’t love trees? Who doesn’t love data? The tricky part is combining them together in a useful way. In this programming explanation / tutorial we’ll take a look at an extremely versatile and useful type of tree... no, not oak - **Quad Trees**! 

*Difficulty: Medium (Intermediate programming knowledge helpful, understanding of Unity and how its GameObject system works useful in the example scene)*

## The Theory:
A _**quad tree**_ is a data structure made up of elements *(commonly called __nodes__)* that are each able to divide itself into 4 child nodes, recursively. Starting from a single _**root node**_, it is able to continually partition itself out into four smaller sections. This parent - child relationship is core to the structure of the tree. 

![image](https://user-images.githubusercontent.com/90412342/140616604-8489d971-1164-4564-817a-91c5567f6d29.png)

Quad trees, like many data structures, are broadly applicable to many situations, from Level-of-Detail (LOD) models, to organizing complex related data, to even image compression algorithms! 

This subdivision is (in theory) able to continue ad infinitum, but in a practical application I *highly* suggest you set an upper limit for how many generations can be spawned, lest you get stuck in infinite recursion. That’s a scary place to be.

## The Practice:
Now that we’ve laid out how such a tree should work in theory, we can take a look at how it might be programmed and put into use. To give an example of how to set up a quad tree and how it might physically be represented, I’ve created an example scene in Unity. 

To start with, naturally we will need a couple of classes. I’ve found it useful to make each quad (or node) into its own object, which the quad tree class then catalogues and operates on.

![scripts](https://user-images.githubusercontent.com/90412342/140616667-509c3351-65fc-4987-96d9-fb0eb5db8c5e.png)

First, let’s look at the Quad class.

![quadclass](https://user-images.githubusercontent.com/90412342/140616677-9ecac33e-515a-4317-a2e3-46c357a3b7f0.png)

It contains the following fields:

* __quadObject__ - The Unity GameObject associated with this quad. (may not be applicable to purely data focused or other implementations. Make a quad class that suits your own needs!)
* __center__ - The Unity Vector3 position of this quad. (again, if using a less physical based implementation holding this information would not be necessary)
* __generation__ - An integer representing how far down the tree this quad exists in. (useful for quick referencing where this quad exists and who its relatives are)
* __limit__ - A reference to a high-level variable representing how many generations the quad tree may spawn.
* __parent__ - A reference to the quad that spawned this quad. (the root node’s parent field will be null)
* __children__ - A fixed-size array of quads containing references to its children.

And the following methods:

* __Constructor__ -  Accepts the necessary fields to populate newly created quads. In all cases except the root node, this method will be called directly from inside the parent quad.
* __Subdivide__ - The method that allocates and creates each new child quad. The information provided to the children here can be specified on a per-child basis.

![subdivide](https://user-images.githubusercontent.com/90412342/140616771-72adf234-133b-4556-8d55-e0d3448fc8ef.png)

* __GetDescendants__ - This method returns an array populated with all descendants from that node onward, following the branches of each child and each of their children.

![getdescendants](https://user-images.githubusercontent.com/90412342/140616802-1b7db55e-f6e7-45f5-9007-1e5a21da6aae.png)

Now, let’s look at the actual QuadTree class.

![quadtreeclass](https://user-images.githubusercontent.com/90412342/140616832-8abdd04d-31fa-4d8e-84cb-a444c37eba78.png)

It has the following fields:

* __nodes__ - A simple list containing each node. All of the parent - child information is contained within the nodes themselves, relieving the tree from having to keep track of the relationships.
* __limit__ - The integer defining how many generations this tree may recursively produce.
* 
And the following methods:

* __Constructor__ - Simply takes in the limit integer.
* __GenerateTree__ - The public method that kickstarts the quad tree generation from an external caller.
* __AddRoot__ - A private method that declares and initializes the root node at index 0.

![addroot](https://user-images.githubusercontent.com/90412342/140616890-d1b0f873-b976-4b48-bd0b-ccba29f3f0a0.png)

* __AddGenerations__ - A private method that starts recursive generation of successive quad generations by subdividing the root node to start. Finally, it adds the entire descendant node tree to the list.

![addgenerations](https://user-images.githubusercontent.com/90412342/140616915-b2528928-5135-457e-990e-b7800ffedbf9.png)

That’s all there is to it! This implementation contains a healthy mix of generic adaptable concepts with some specific to Unity _(merely to spawn geometric Quads in-scene to visualize the arrangement of the tree.)_

With a little extra tweaks and a proper MonoBehaviour script, the end result in Unity looks like this: 

![QuadTreeGenerations](https://user-images.githubusercontent.com/90412342/138600389-981a8b6b-cfeb-49ca-8fe4-21cd6b5056ef.gif)

_(A quad recursively divided from 1 to 8 generations)_

## Conclusion:
All code from this example is open-source and available to be learned from or used. Hopefully someone finds this information valuable. It would be very easy to branch this project off into an octree or a binary tree from here, simply by reworking how many children are made and placed.

Possible Improvements:

* Improve performance!! This is by no means the most clean or efficient implementation of the concept, merely an introduction
* Allow for different generation limits per-node rather than globally set
* Make the entire quad tree class generic, for use with different data types


