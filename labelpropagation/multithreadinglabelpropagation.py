import networkx as nx
import multiprocessing
from itertools import repeat
from tqdm import tqdm

def calculate_shortest_distance(community, G, target_node):
    try:
        return min(nx.shortest_path_length(G, source=node, target=target_node) for node in community)
    except nx.NetworkXNoPath:
        return float('inf')

def main():
    # Load the network from edgelist
    G = nx.read_edgelist('C:\CsharpCsharp\Degrees_of_Hitler\wiki-topcats.txt', create_using=nx.DiGraph())

    # Apply Label Propagation Algorithm
    communities = nx.algorithms.community.label_propagation.label_propagation_communities(G)
    community_list = list(communities)

    # Setup multiprocessing
    pool = multiprocessing.Pool(processes=multiprocessing.cpu_count())
    target_node = '1159788'

    # Parallel calculation of shortest paths with progress bar
    with tqdm(total=len(community_list)) as pbar:
        for _ in pool.starmap(calculate_shortest_distance, zip(community_list, repeat(G), repeat(target_node))):
            pbar.update()

    pool.close()
    pool.join()

    # Sorting and displaying the results can be done here as in the previous script

if __name__ == "__main__":
    main()
