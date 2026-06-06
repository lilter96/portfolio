import { useQuery } from "@tanstack/react-query";
import { fetchGitHubRepos } from "@/lib/api";

export function useGitHubRepos() {
  return useQuery({
    queryKey: ["github-repos"],
    queryFn: fetchGitHubRepos,
    staleTime: 5 * 60 * 1000,
  });
}
