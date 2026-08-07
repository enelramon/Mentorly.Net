public interface IPeerReviewRepository
{
    Task<PeerReview[]> GetAllAsync(CancellationToken cancellationToken = default);

    Task<PeerReview?> GetByIdAsync(Guid peerReviewId, CancellationToken cancellationToken = default);

    Task<PeerReview[]> GetBySubmissionIdAsync(
        Guid submissionId,
        CancellationToken cancellationToken = default);

    Task<PeerReview[]> GetByReviewerStudentIdAsync(
        Guid reviewerStudentId,
        CancellationToken cancellationToken = default);

    Task<bool> HasReviewerAlreadyReviewedAsync(
        Guid submissionId,
        Guid reviewerStudentId,
        CancellationToken cancellationToken = default);

    Task<int> CountApprovalsForSubmissionAsync(
        Guid submissionId,
        CancellationToken cancellationToken = default);

    Task AddAsync(PeerReview review, CancellationToken cancellationToken = default);

    Task UpdateAsync(PeerReview peerReview, CancellationToken cancellationToken = default);

    Task DeleteAsync(PeerReview peerReview, CancellationToken cancellationToken = default);
}