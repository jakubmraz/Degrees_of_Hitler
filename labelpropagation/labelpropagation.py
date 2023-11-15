import networkx as nx

# Step 1: Load the network from edgelist
G = nx.read_edgelist('C:\CsharpCsharp\Degrees_of_Hitler\wiki-topcats.txt', create_using=nx.DiGraph())

# Step 2: Apply Label Propagation Algorithm
communities = nx.algorithms.community.label_propagation.label_propagation_communities(G)
community_dict = {frozenset(community): idx for idx, community in enumerate(communities)}

# Step 3: Calculate shortest path distances to node 1159788
target_node = '1159788'
distances = {}
for community, idx in community_dict.items():
    try:
        distance = min(nx.shortest_path_length(G, source=node, target=target_node) for node in community)
    except nx.NetworkXNoPath:
        distance = float('inf')
    distances[idx] = distance

# Step 4: Sort communities by distance
sorted_communities = sorted(distances.items(), key=lambda x: x[1])

# Step 5: Get the top 20 and bottom 20 communities
best_20 = sorted_communities[:20]
worst_20 = sorted_communities[-20:]

print("Best 20 Communities to get to node 1159788:")
for community, distance in best_20:
    print(f"Community {community}, Distance: {distance}")

print("\nWorst 20 Communities (furthest from node 1159788):")
for community, distance in worst_20:
    print(f"Community {community}, Distance: {distance}")
